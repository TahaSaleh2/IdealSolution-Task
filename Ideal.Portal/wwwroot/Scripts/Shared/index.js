
$(document).ready(function () {
    //var toggleUpdateModal = function () {
    //    $("#updateModal").modal("toggle");
    //}
});

var ShowToastr = function (type, message, title) {
    toastr[type](message, title, { "progressBar": true, "closeButton": true, "positionClass": localizer.ToastrPosition });
}

var Message = {
    Success : function (message, title) {
        ShowToastr('success', message, title);
    },
    Info : function (message, title) {
        ShowToastr('info', message, title);
    },
    Error : function (message, title) {
        ShowToastr('error', message, title);
    },
    Warning : function (message, title) {
        ShowToastr('warning', message, title);
    }
}

var AjaxSuccess = function (xhr) {
    Message.Success(xhr.message, xhr.title);
}
var AjaxError = function (xhr) {
    Message.Error(xhr.message, xhr.title);
}

var toggleCreateModal = function () {
    $("#createModal").modal("toggle");
}
var toggleViewModal = function () {
    $("#ViewModal").modal("toggle");
}
var toggleEditModal = function () {
    $("#editModal").modal("toggle");
}
var toggleDeleteModal = function () {
    $("#deleteModal").modal("toggle");
}

var loading = function () {
    var spinner = $('#loader');
    spinner.toggle();
}
var activePage = function (pageLink, pageMenu) {

    $("#" + pageLink).addClass("active");

    if (pageMenu) {
        $("#" + pageMenu).addClass("menu-open");
        $("#" + pageMenu + ">a").addClass("active");
    }
}

var toDecimal = function (value) {
    var num = Number(value);
    num = Math.round(num * 100) / 100;
    return num;
}

var toDecimalString = function (value) {
    var num = Number(value);
    return num.toFixed(2);
}
