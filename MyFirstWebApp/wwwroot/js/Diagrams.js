var popCanvas = $("#popChart");

let shopsData = [];
for (var i = 0; i < model.shopNames.length; i++) {
    shopsData.push(model.shopNames[i]);
}
console.log(shopsData);

let shopIncomeData = [];
for (var i = 0; i < model.income.length; i++) {
    shopIncomeData.push(model.income[i]);
}
console.log(shopIncomeData);

var barChart = new Chart(popCanvas, {
    type: 'bar',
    data: {
        labels: shopsData,
        datasets: [{
            label: 'Доходы магазинов',
            data: shopIncomeData,
            backgroundColor: [
                'rgba(255, 99, 132, 0.6)',
                'rgba(54, 162, 235, 0.6)',
                'rgba(255, 206, 86, 0.6)',
                'rgba(75, 192, 192, 0.6)',
                'rgba(153, 102, 255, 0.6)',
                'rgba(255, 159, 64, 0.6)',
            ]
        }]
    },

});


var popCanvasPie = $("#popChartPie");

var pieChart = new Chart(popCanvasPie, {
    type: 'pie',
    data: {
        labels: shopsData,
        datasets: [{
            label: 'Доход магазина',
            data: shopIncomeData,
            backgroundColor: [
                'rgba(255, 99, 132, 0.6)',
                'rgba(54, 162, 235, 0.6)',
                'rgba(255, 206, 86, 0.6)',
                'rgba(75, 192, 192, 0.6)',
                'rgba(153, 102, 255, 0.6)',
                'rgba(255, 159, 64, 0.6)',
                'rgba(255, 99, 132, 0.6)',
                'rgba(54, 162, 235, 0.6)',
                'rgba(255, 206, 86, 0.6)',
                'rgba(75, 192, 192, 0.6)',
                'rgba(153, 102, 255, 0.6)'
            ]
        }]
    },
    options: {
        tooltips: {
            enabled: false
        },
        plugins: {
            datalabels: {
                color: '#111',
                textAlign: 'center',
                font: {
                    lineHeight: 1.6
                },
                formatter: function (value) {
                    return value;
                }
                }
            }
        
    }
});