

    var salesData = {
        labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
    datasets: [
    {
        label: "Ventas",
            backgroundColor: "#007bff",
    borderColor: "rgba(75, 192, 192, 1)",
            data: [65.000, 59.000, 80.000, 81.000, 56.000, 55.000, 85.000, 15.000, 105.000, 55.000, 25.000, 75.000],
            },
    ],
    };

    var ctx = document.getElementById("sales-chart").getContext("2d");


    var salesChart = new Chart(ctx, {
        type: "bar",
    data: salesData,
    options: {
        scales: {
        y: {
        beginAtZero: true,
                },
            },
        },
    });


    function updateChartData() {

        salesChart.update();
    }


    setInterval(updateChartData, 5000);

