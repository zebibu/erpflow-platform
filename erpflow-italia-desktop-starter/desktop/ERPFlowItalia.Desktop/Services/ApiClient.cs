using System.Net.Http;
using System.Net.Http.Json;

namespace ERPFlowItalia.Desktop.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly Uri[] _baseAddresses =
    {
        new("https://localhost:7000/api/"),
        new("http://localhost:5000/api/")
    };

    public ApiClient()
    {
        var handler = new HttpClientHandler
        {
            // For local development only. In production, use a valid HTTPS certificate.
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = _baseAddresses[0]
        };
    }

    private async Task<T?> TryGetFromAvailableApisAsync<T>(string endpoint)
    {
        foreach (var baseAddress in _baseAddresses)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T>(new Uri(baseAddress, endpoint));
            }
            catch
            {
                // Try the next local API endpoint.
            }
        }

        return default;
    }

    private async Task<bool> TryPostToAvailableApisAsync<T>(string endpoint, T body)
    {
        foreach (var baseAddress in _baseAddresses)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(new Uri(baseAddress, endpoint), body);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch
            {
                // Try the next local API endpoint.
            }
        }

        return false;
    }

    private async Task<bool> TryPostToAvailableApisAsync(string endpoint)
    {
        foreach (var baseAddress in _baseAddresses)
        {
            try
            {
                var response = await _httpClient.PostAsync(new Uri(baseAddress, endpoint), null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch
            {
                // Try the next local API endpoint.
            }
        }

        return false;
    }

    private async Task<bool> TryPutToAvailableApisAsync<T>(string endpoint, T body)
    {
        foreach (var baseAddress in _baseAddresses)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(new Uri(baseAddress, endpoint), body);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch
            {
                // Try the next local API endpoint.
            }
        }

        return false;
    }

    private async Task<bool> TryPutToAvailableApisAsync(string endpoint)
    {
        foreach (var baseAddress in _baseAddresses)
        {
            try
            {
                var response = await _httpClient.PutAsync(new Uri(baseAddress, endpoint), null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch
            {
                // Try the next local API endpoint.
            }
        }

        return false;
    }

    private async Task<bool> TryDeleteFromAvailableApisAsync(string endpoint)
    {
        foreach (var baseAddress in _baseAddresses)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(new Uri(baseAddress, endpoint));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch
            {
                // Try the next local API endpoint.
            }
        }

        return false;
    }

    public async Task<List<T>> GetListAsync<T>(string endpoint)
    {
        var data = await TryGetFromAvailableApisAsync<List<T>>(endpoint);
        return data ?? new List<T>();
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        return await TryGetFromAvailableApisAsync<T>(endpoint);
    }

    public async Task<bool> PostAsync<T>(string endpoint, T body)
    {
        return await TryPostToAvailableApisAsync(endpoint, body);
    }

    public async Task<bool> PostAsync(string endpoint)
    {
        return await TryPostToAvailableApisAsync(endpoint);
    }

    public async Task<bool> PutAsync<T>(string endpoint, T body)
    {
        return await TryPutToAvailableApisAsync(endpoint, body);
    }

    public async Task<bool> PutAsync(string endpoint)
    {
        return await TryPutToAvailableApisAsync(endpoint);
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        return await TryDeleteFromAvailableApisAsync(endpoint);
    }
}
