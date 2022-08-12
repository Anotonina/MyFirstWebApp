



function bubbleSort(propetryName, elem) {
    
        let isDescDirection = false;
        model.table = model.table || {};
        if (model.table[propetryName]) {
            isDescDirection = model.table[propetryName].isDescDirection;
        } else {
            model.table[propetryName] = { isDescDirection: false };
        }
        for (i = 0; i < model.shops.length; i++) {
            for (j = i + 1; j < model.shops.length; j++) {
                let resultComparasing = false;
                if (!isDescDirection) {
                    resultComparasing = model.shops[i][propetryName] > model.shops[j][propetryName];

                } else {
                    resultComparasing = model.shops[i][propetryName] < model.shops[j][propetryName];
                }

                if (resultComparasing) {
                    temp = model.shops[i];
                    model.shops[i] = model.shops[j];
                    model.shops[j] = temp;
                    getSortShops();
                }
            }
        }
        model.table[propetryName].isDescDirection = !isDescDirection;
        elem.textContent = model.table[propetryName].isDescDirection ? "Sort(Desc)" : "Sort(Asc)"
       
}
    function getSortShops() {

        $('.table tbody').html("");
        for (var i = 0; i < model.shops.length; i++) {

            let shop = model.shops[i];
            $('.table tbody').append("<tr>" +
                "<td>" +
                '<a href="/Cashiers/Index/' + shop.id + '">' + shop.shopName + '</a>' +
                "</td>" +
                "<td>" + shop.sale + "</td>" +
                 "<td>" + shop.shopIncome + "</td>" +
                "<td><p class='controls'><input type='checkbox' onclick='selectedCheckbox()' class='needCheck' name='checkShop' value='" + shop.id + "' id='check'/></p></td>" +
                "</tr>");
        }


    }

    //1.Create sort bubble function
    //2) subscribe on click event and execute finction via JQuery
    //2.1 Rerender table based on sorted data

    function onClick() {
        $.ajax({
            type: "POST",
            url: "/Home/SetShops",
            data: JSON.stringify({ ShopName: $("[name='ShopName']").val(), Sale: $("[name='Sale']").val(), ShopIncome: $("[name='ShopIncome']").val() }),
            success: getShops,
            contentType: "application/json"
        })

    }


    function getShops() {
        var saleFrom = $('[name="shopFiltrViwModel.SaleFrom"]').val();
        var saleTo = $('[name="shopFiltrViwModel.SaleTo"]').val();
        var shopNameFiltr = $('[name="shopFiltrViwModel.shopNameFiltr"]').val();
        var url = "/Home/GetShops?page=" + model.pageInfo.pageNumber;
        if (saleFrom)
            url += "&saleFrom=" + saleFrom;
        if (saleTo)
            url += "&saleTo=" + saleTo;
        if (shopNameFiltr)
            url += "&shopNameFiltr=" + shopNameFiltr
        $.get(url, (indexViewModel) => {
            $('.table tbody').html("");

            for (var i = 0; i < indexViewModel.shops.length; i++) {
                let shop = indexViewModel.shops[i];
                $('.table tbody').append("<tr>" +
                    "<td>" +
                    '<a href="/Cashiers/Index/' + shop.id + '">' + shop.shopName + '</a>' +
                    "</td>" +
                    "<td>" + shop.sale + "</td>" +
                    "<td>" + shop.shopIncome + "</td>" +
                    "<td><p class='controls'><input type='checkbox' onclick='selectedCheckbox()' class='needCheck' name='checkShop' value='" + shop.id + "' id='check'/></p></td>" +
                    "</tr>");
            }

            var $helper = $("#helper");
            $helper.html("");
            for (var i = 0; i < indexViewModel.pageInfo.totalPages; i++) {
                let page = i + 1;
                if (page == model.pageInfo.pageNumber) {
                    $helper.append('<a class="btn btn-default selected btn-primary" href="/Home/AddShops?Page=' + page + '">' + page + '</a>');
                } else {
                    $helper.append('<a class="btn btn-default " href="/Home/AddShops?Page=' + page + '">' + page + '</a>');
                }
            }
            selectedCheckbox();
            model = indexViewModel;

        })
    }
    var selectedItems = [];
    function selectedCheckbox() {
        selectedItems = [];
        $("input[id='check']:checked").each(function () {
            selectedItems.push($(this).val());
            $('#result span').html(selectedItems.length);
        });
        $('#result span').html(selectedItems.length);

        var all = document.querySelectorAll(".controls .needCheck"),
            suball = all.length;


        if (selectedItems.length < suball) {
            $('#checkbox').prop('checked', false)
        };

        if (selectedItems.length == suball) {
            $('#checkbox').prop('checked', true)
        };
        if (suball == 0) {
            $('#checkbox').prop('checked', false)
        };
    }



    function onClickDelete() {
        $.ajax({
            type: "POST",
            url: "/Home/Delete",
            data: JSON.stringify(selectedItems),
            success: getShops,
            contentType: "application/json"
        })
    }



    $('#checkbox').click(function () {
        if ($(this).is(':checked')) {
            $("input[id='check']").each(function () {
                $(this).prop('checked', true)
            });
            selectedItems = [];
            $("input[id='check']:checked").each(function () {
                selectedItems.push($(this).val());
                $('#result span').html(selectedItems.length);

            });
        } else {
            $("input[id='check']").each(function () {
                $(this).prop('checked', false);
                selectedItems.length = 0;
                $('#result span').html(selectedItems.length);
            })
        }
    });

