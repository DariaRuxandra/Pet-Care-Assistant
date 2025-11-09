using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;
using SkiaSharp;
using Pet_Care_Assistant.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class AppointmentStatsViewModel : ObservableObject
    {
        [ObservableProperty]
        Chart appointmentsPerDayChart;

        [ObservableProperty]
        Chart dogsPerClinicChart;

        public AppointmentStatsViewModel(ObservableCollection<Appointment> appointments)
        {
            GenerateCharts(appointments);
        }

        private void GenerateCharts(ObservableCollection<Appointment> appointments)
        {
            if (appointments == null || !appointments.Any())
                return;

            // --- Appointments per day ---
            var groupedByDay = appointments
                .GroupBy(a => a.Date.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToList();

            this.AppointmentsPerDayChart = new BarChart
            {
                Entries = groupedByDay.Select(g => new ChartEntry(g.Count)
                {
                    Label = g.Date.ToString("dd MMM"),
                    ValueLabel = g.Count.ToString(),
                    Color = SKColor.Parse("#4CAF50")
                }).ToList(),
                LabelTextSize = 30
            };

            // --- Dogs per clinic (with distinct colors) ---
            var groupedByClinic = appointments
                .GroupBy(a => a.ClinicName)
                .Select(g => new { Clinic = g.Key, Count = g.Count() })
                .ToList();

            // Define a palette of nice distinct colors
            var colorPalette = new[]
            {
                "#2196F3", // blue
                "#FF9800", // orange
                "#9C27B0", // purple
                "#4CAF50", // green
                "#E91E63", // pink
                "#00BCD4", // cyan
                "#FFC107", // amber
                "#8BC34A", // light green
                "#FF5722", // deep orange
                "#3F51B5"  // indigo
            };

            int colorIndex = 0;

            this.DogsPerClinicChart = new DonutChart
            {
                Entries = groupedByClinic.Select(g => new ChartEntry(g.Count)
                {
                    Label = g.Clinic,
                    ValueLabel = g.Count.ToString(),
                    Color = SKColor.Parse(colorPalette[colorIndex++ % colorPalette.Length])
                }).ToList(),
                LabelTextSize = 30
            };
        }
    }
}
