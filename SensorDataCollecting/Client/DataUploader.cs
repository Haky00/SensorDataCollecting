using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SensorDataCollecting.Client.Shared;
using SensorDataCollecting.Shared;
using System.Net.Http.Json;

namespace SensorDataCollecting.Client;

public class DataUploader
{
    private HttpClient _httpClient;
    private ILocalStorageService _localStorage;
    private IDialogService _dialogService;

    public int Total;
    public int Current;
    public int Success;
    public List<HttpResponseMessage> Responses = new();
    public EventCallback StateChanged;

    public DataUploader(HttpClient httpClient, ILocalStorageService localStorage, IDialogService dialogService)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _dialogService = dialogService;
    }

    private async Task<HttpResponseMessage> Upload(SensorDataWrapper data)
    {
        await Task.Delay(2000);
        return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
    }

    //private Task<HttpResponseMessage> Upload(SensorDataWrapper data) =>
    //    _httpClient.PostAsJsonAsync("/Api/SensorData", data.SensorData);

    public async Task UploadDataMultiple(IEnumerable<SensorDataWrapper> dataList)
    {
        Total = dataList.Count();
        Current = 0;
        Success = 0;

        var parameters = new DialogParameters<UploadDialog> { { x => x.Uploader, this } };
        var dialog = await _dialogService.ShowAsync<UploadDialog>("Nahrávání souborů", parameters);

        foreach (var data in dataList)
        {
            HttpResponseMessage response = await Upload(data);
            Responses.Add(response);
            if (response.IsSuccessStatusCode)
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
        Total = 1;
        Current = 0;
        Success = 0;

        var parameters = new DialogParameters<UploadDialog> { { x => x.Uploader, this } };
        var dialog = await _dialogService.ShowAsync<UploadDialog>("Nahrávání souborů", parameters);

        HttpResponseMessage response = await Upload(data);
        Responses.Add(response);
        if (response.IsSuccessStatusCode)
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
