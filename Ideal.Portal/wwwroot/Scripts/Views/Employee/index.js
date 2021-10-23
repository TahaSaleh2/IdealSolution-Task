$(document).ready(function () {
    activePage("Employee-link");
    setDataTable();
});

var setDataTable = function () {
    $("#pagging").toggle();
    _$table = $('#resultTable');
    var _$usersTable = _$table.DataTable({
        paging: true,
        searching: true,
        serverSide: true,
        //responsive: true,
        language: {
            "search": "Search :",
            "paginate": {
                "first": "First",
                "last":  "Last",
                "next": "Next",
                "previous": "Previous"
            }
        },
        aLengthMenu: [
            [5, 10, 25, 50, 100, 150, 200, 500, -1],
            [5, 10, 25, 50, 100, 150, 200, 500]
        ],
        ajax: function (data, callback, settings) {
            var filter = {
                MaxResultCount: data.length,
                SkipCount: data.start,
                Search: data.search.value
            };
            //loading();
            $.ajax({
                url: "/Employee/EmployeeList",
                dataType: 'json',
                type: 'POST',
                data: filter,
                success: function (result, status, xhr) {   // success callback function
                    callback({
                        recordsTotal: result.totalCount,
                        recordsFiltered: result.totalCount,
                        data: result.items
                    });
                    styleTable();
                },
                error: function (jqXhr, textStatus, errorMessage) { // error callback 
                },
                complete: function () {
                    //loading();
                }

            });

        },
        infoCallback: function (settings, start, end, max, total, pre) {
            pageInfo = this.api().page.info();
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$usersTable.draw(false)
            }
        ],
        columnDefs: [
            //{
            //    targets: 0,
            //    className: 'control',
            //    defaultContent: '',
            //},
            {
                targets: 0,
                data: 'fName',
                sortable: false
            },
            {
                targets: 1,
                data: 'lName',
                sortable: false
            },
            {
                targets: 2,
                data: 'email',
                sortable: false
            },
            {
                targets: 3,
                data: 'jobTitle',
                sortable: false
            },
            {
                targets: 4,
                data: 'salary',
                sortable: false
            },
            {
                targets: 5,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <a class="btn btn-sm m-1 bg-primary" data-ajax="true" data-ajax-loading="#loader"
                           data-ajax-mode="replace" data-ajax-success="toggleEditModal" data-ajax-update="#editBody" href="/Employee/Update/${row.id}">`,
                        `       <i class="fas fa-pencil-alt"></i> Edit`,
                        '   </a>',
                        `    <a class="btn btn-sm m-1 bg-danger" data-ajax="true" data-ajax-loading="#loader"
                           data-ajax-mode="replace" data-ajax-success="toggleDeleteModal" data-ajax-update="#deleteBody" href="/Employee/Delete/${row.id}">`,
                        `       <i class="fas fa-trash"></i> Delete`,
                        '   </a>'
                    ].join('');
                }
            }
        ]
    });

}

var RefreshDataTable = function () {
    var table = $('#resultTable').DataTable();
    table.ajax.reload();
    //var beforePage = pageInfo.page;
    //table.ajax.reload(function () {
    //    if (pageInfo.pages === beforePage) {
    //        table.page(beforePage - 1).draw(false);
    //    } else {
    //        table.page(beforePage).draw(false);
    //    }
    //}, false);


}

var styleTable = function () {
    $("#resultTable_paginate .pagination").addClass('justify-content-end')
    var paginate = $("#resultTable_paginate");
    paginate.removeClass();

    $("#resultTable").css({ "width": "inherit" });
    $("#resultTable thead>tr>th:nth-child(1)").removeClass();

}

var AddedSuccess = function (xhr) {
    RefreshDataTable();
    toggleCreateModal();
    Message.Success(xhr.message, xhr.title);
}

var EditedSuccess = function (xhr) {
    RefreshDataTable();
    toggleEditModal();
    Message.Success(xhr.message, xhr.title);
}

var DeletedSuccess = function (xhr) {
    RefreshDataTable();
    toggleDeleteModal();
    Message.Success(xhr.message, xhr.title);
}