

    var salesData = {
        labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio"],
    datasets: [
    {
        label: "Ventas",
    backgroundColor: "rgba(75, 192, 192, 0.2)",
    borderColor: "rgba(75, 192, 192, 1)",
    data: [65.000, 59.000, 80.000, 81.000, 56.000, 55.000],
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
