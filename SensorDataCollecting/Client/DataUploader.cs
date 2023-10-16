using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Postgrest;
using SensorDataCollecting.Client.Shared;
using System.Text.Json;

namespace SensorDataCollecting.Client;

public class DataUploader
{
    private ILocalStorageService _localStorage;
    private IDialogService _dialogService;
    private ISnackbar _snackbar;

    public int Total;
    public int Current;
    public int Success;
    public List<HttpResponseMessage?> Responses = new();
    public EventCallback StateChanged;

    public DataUploader(ILocalStorageService localStorage, IDialogService dialogService, ISnackbar snackbar)
    {
        _localStorage = localStorage;
        _dialogService = dialogService;
        _snackbar = snackbar;
    }

    private async Task<Supabase.Client> SupabaseConnect()
    {
        var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
        var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = false,
        };

        var supabase = new Supabase.Client(url!, key, options);
        await supabase.InitializeAsync();
        return supabase;
    }

    private async Task<UserInfo> GetUserInfo()
    {
        if (await _localStorage.ContainKeyAsync("userInfo"))
        {
            return await _localStorage.GetItemAsync<UserInfo>("userInfo");
        }
        return new();
    }

    private async Task<HttpResponseMessage?> Upload(SensorDataWrapper data, Supabase.Client db, UserInfo userInfo) =>
        (await db.From<SensorDataDb>().Insert(data.ToDbWrapper(userInfo), new QueryOptions() { Returning = QueryOptions.ReturnType.Minimal})).ResponseMessage;

    public async Task UploadDataMultiple(IEnumerable<SensorDataWrapper> dataList)
    {
        Supabase.Client db = await SupabaseConnect();
        UserInfo userInfo = await GetUserInfo();
        Total = dataList.Count();
        Current = 0;
        Success = 0;

        var parameters = new DialogParameters<UploadDialog> { { x => x.Uploader, this } };
        var dialog = await _dialogService.ShowAsync<UploadDialog>("Nahrávání souborů", parameters);
        try
        {
            foreach (var data in dataList)
            {
                HttpResponseMessage? response = await Upload(data, db, userInfo);
                Responses.Add(response);
                if (response is not null && response.IsSuccessStatusCode)
                {
                    Current++;
                    Success++;
                    data.IsUploaded = true;
                    await _localStorage.SetItemAsync("data" + data.Id, data);
                    await StateChanged.InvokeAsync(data);
                }
            }

            dialog.Close();
        }
        catch
        {
            dialog.Close();
            _snackbar.Add("Nahrávání se nezdařilo. Zkontrolujte připojení k internetu.", Severity.Error);
        }
    }

    public async Task UploadData(SensorDataWrapper data)
    {
        Supabase.Client db = await SupabaseConnect();
        UserInfo userInfo = await GetUserInfo();
        Total = 1;
        Current = 0;
        Success = 0;

        var parameters = new DialogParameters<UploadDialog> { { x => x.Uploader, this } };
        var dialog = await _dialogService.ShowAsync<UploadDialog>("Nahrávání souborů", parameters);
        try
        {
            HttpResponseMessage? response = await Upload(data, db, userInfo);
            Responses.Add(response);
            if (response is not null && response.IsSuccessStatusCode)
            {
                Current++;
                Success++;
                data.IsUploaded = true;
                await _localStorage.SetItemAsync("data" + data.Id, data);
                await StateChanged.InvokeAsync(data);
            }

            dialog.Close();
        }
        catch
        {
            dialog.Close();
            _snackbar.Add("Nahrávání se nezdařilo. Zkontrolujte připojení k internetu.", Severity.Error);
        }
    }
}
