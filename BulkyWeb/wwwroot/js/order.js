﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable
    (
    {
        "ajax": { url: '/admin/order/getall' },
        "columns":
            [
                { data: 'id', "width": "5%" },
                { data: 'name', "width": "25%" },
                { data: 'phoneNumber', "width": "20%" },
                { data: 'applicationUser.email', "width": "20%" },
                { data: 'orderStatus', "width": "10%" },
                { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return ` <div class="w-75 btngroup" role="group">
                        <a href="/admin/order/upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a
                    </div >'
                },
                "width": "25%"
            }
            ]
    });
}


    //{ data: 'id', "width": "5%" },
    //{ data: 'name', "width": "25%" },
    //{ data: 'phoneNumber', "width": "20%" },
    //{ data: 'applicationUser.email', "width": "20%" },
    //{ data: 'orderStatus', "width": "10%" },
    //{ data: 'orderTotal', "width": "10%" },
    //{




    //function loadDataTable(status) {
    //    dataTable = $('#tblData').DataTable({
    //        "ajax": { url: '/admin/order/getall?status=' + status },
    //        "columns": [
    //            { data: 'id', "width": "5%" },
    //            { data: 'name', "width": "25%" },
    //            { data: 'phoneNumber', "width": "20%" },
    //            { data: 'applicationUser.email', "width": "20%" },
    //            { data: 'orderStatus', "width": "10%" },
    //            { data: 'orderTotal', "width": "10%" },
    //            {
    //                data: 'id',
    //                "render": function (data) {
    //                    return `<div class="w-75 btn-group" role="group">
    //                 <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>               
                    
    //                </div>`
    //                },
    //                "width": "10%"
    //            }
    //        ]
    //    });
}






