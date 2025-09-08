using ReactiveUI;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive;
using System.Threading.Tasks;

namespace ValveBench.Desktop.ViewModels;

public partial class MainWindowViewModel : ReactiveObject
{
    private double _p1 = 100;

    private double _p2 = 120;

    private int _cycles = 50;

    private double _tempC = 60;

    private string _result = "";

    private bool _isBusy;

    public double P1 { get => _p1; set => this.RaiseAndSetIfChanged(ref _p1, value); }

    public double P2 { get => _p2; set => this.RaiseAndSetIfChanged(ref _p2, value); }

    public int Cycles { get => _cycles; set => this.RaiseAndSetIfChanged(ref _cycles, value); }

    public double TempC { get => _tempC; set => this.RaiseAndSetIfChanged(ref _tempC, value); }

    public string Result { get => _result; set => this.RaiseAndSetIfChanged(ref _result, value); }

    public bool IsBusy { get => _isBusy; set { this.RaiseAndSetIfChanged(ref _isBusy, value); this.RaisePropertyChanged(nameof(IsIdle)); } }

    public bool IsIdle => !IsBusy;

    public ReactiveCommand<Unit, Unit> CalculateCommand { get; }


    private readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:8080") };

    public MainWindowViewModel()
    {
        
        CalculateCommand = ReactiveCommand.CreateFromTask(CalculateAsync);
    }

    private async Task CalculateAsync()
    {
        var body = new { p1Bar = P1, p2Bar = P2, cycles = Cycles, tempC = TempC };

        try
        {
            IsBusy = true;
            var resp = await _http.PostAsJsonAsync("api/run", body);
            resp.EnsureSuccessStatusCode();
            var res = await resp.Content.ReadFromJsonAsync<ResultDto>();
            Result = $"Peal: {res!.PeakPressureBar} bar | Drop: {res.PressureDropBar} bar | Leak: {res.LeakDetected}";
        }
        catch (Exception ex)
        {
            Result = $"Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private record ResultDto(double PeakPressureBar, double PressureDropBar, bool LeakDetected);

    public string Title { get; } = "Welcome to ValveBench!";
}
