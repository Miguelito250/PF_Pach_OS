var data = {
    labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo"],
    datasets: [{
        label: "Ventas",
        data: [12, 19, 3, 5, 2],
        backgroundColor: "#007bff",
        borderColor: "#007bff",
        borderWidth: 1
    }]
};

var cts = document.getElementById("visitors-chart").getContext("2d");

var myChart = new Chart(cts, {
    type: "line",  
    data: data,   
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});