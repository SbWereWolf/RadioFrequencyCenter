﻿function Common() {
    _this = this;

    this.init = function () {
        $("#LoginPopup").click(function () {
            _this.showPopup("/RadioDevice/RecordPopup/521", initLoginPopup);
        });
    }

    this.showPopup = function (url, callback)
    {
        $.ajax({
            type : "GET",
            url: url,
            success: function (data)
            {
                showModalData(data, callback);
            }
        });
    }

    function initLoginPopup(modal) {
        $("#LoginButton").click(function () {
            //$.ajax({
            //    type: "POST",
            //    url: "/RadioDevice/RecordPopup/521",
            //    data : $("#LoginForm").serialize(),
            //    success: function (data) {
            //        showModalData(data);
            //        initLoginPopup(modal);
            //    }
            //});
        });
    }

    function showModalData(data, callback) {
        $(".modal-backdrop").remove();
        var popupWrapper = $("#PopupWrapper");
        popupWrapper.empty();
        popupWrapper.html(data);
        var popup = $(".modal", popupWrapper);
        $(".modal", popupWrapper).modal();
        if (callback != undefined) {
            callback(popup);
        }
    }
}

var common = null;
$().ready(function () {
    common = new Common();
    common.init();
});

function showPopup(dataUrl) {
    $.ajax({
        type: "GET",
        url: dataUrl,
        success: function (data) {
            showModalData(data);
        }
    });
}

function showModalData(data) {
    $(".modal-backdrop").remove();
    var popupWrapper = $("#PopupWrapper");
    popupWrapper.empty();
    popupWrapper.html(data);
    $(".modal", popupWrapper).modal();
}
