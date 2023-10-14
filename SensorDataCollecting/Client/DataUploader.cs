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

    public int Total;
    public int Current;
    public int Success;
    public List<HttpResponseMessage?> Responses = new();
    public EventCallback StateChanged;

    public DataUploader(ILocalStorageService localStorage, IDialogService dialogService)
    {
        _localStorage = localStorage;
        _dialogService = dialogService;
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

    private async Task<HttpResponseMessage?> Upload(SensorDataWrapper data, Supabase.Client db)
    {
        QueryOptions queryOptions = new() { Returning = QueryOptions.ReturnType.Minimal };

        SensorDataJson dataJson = data.SensorData.ToJsonData();
        dataJson.Id = data.DataInfo.Id;
        var a = await db.From<DataInfo>().Upsert(data.DataInfo, queryOptions);
        if (a.ResponseMessage is null || !a.ResponseMessage.IsSuccessStatusCode)
        {
            return a.ResponseMessage;
        }
        var b = await db.From<SensorDataJson>().Upsert(dataJson, queryOptions);

        return b.ResponseMessage;
    }

    //private Task<HttpResponseMessage> Upload(SensorDataWrapper data) =>
    //    _httpClient.PostAsJsonAsync("/Api/SensorData", data.SensorData);

    public async Task UploadDataMultiple(IEnumerable<SensorDataWrapper> dataList)
    {
        Supabase.Client db = await SupabaseConnect();

        Total = dataList.Count();
        Current = 0;
        Success = 0;

        var parameters = new DialogParameters<UploadDialog> { { x => x.Uploader, this } };
        var dialog = await _dialogService.ShowAsync<UploadDialog>("Nahrávání souborů", parameters);

        foreach (var data in dataList)
        {
            HttpResponseMessage? response = await Upload(data, db);
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

    public async Task UploadData(SensorDataWrapper data)
    {
        Supabase.Client db = await SupabaseConnect();

        Total = 1;
        Current = 0;
        Success = 0;

        var parameters = new DialogParameters<UploadDialog> { { x => x.Uploader, this } };
        var dialog = await _dialogService.ShowAsync<UploadDialog>("Nahrávání souborů", parameters);

        HttpResponseMessage? response = await Upload(data, db);
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
}
