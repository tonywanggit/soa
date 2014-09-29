//--demo.js

$(document).ready(function () {
    function Invoke() {
        var methodType = $("#selMethodType").val();
        var dataType = $("#selDataType").val();
        var methodName = $("#txtMethodName").val();
        var serviceName = $("#txtServiceName").val();
        var message = $("#txtRequest").val();
        var noCache = $("#selNoCache").val();

        methodName = methodType + ":" + dataType + ":" + methodName;

        $("#txtResponse").val("");

        var st = new Date().getTime();

        $.esb.invoke(serviceName, methodName, message, function (response) {
            var duration = new Date().getTime() - st;

            $("#txtResponse").val(response);
            $("#lblDuration").empty().append("耗时：" + duration + "毫秒");
        }, noCache);

    };

    function InvokeQueue() {
        var methodType = $("#selMethodType").val();
        var dataType = $("#selDataType").val();
        var methodName = $("#txtMethodName").val();
        var serviceName = $("#txtServiceName").val();
        var message = $("#txtRequest").val();

        methodName = methodType + ":" + dataType + ":" + methodName;

        $("#txtResponse").val("");

        var st = new Date().getTime();

        $.esb.invokeQueue(serviceName, methodName, message, function (response) {
            var duration = new Date().getTime() - st;

            $("#txtResponse").val(response);
            $("#lblDuration").empty().append("耗时：" + duration + "毫秒");
        });

    };

    $("#btnInvoke").click(function () {
        Invoke();
    });

    $("#btnInvokeQueue").click(function () {
        InvokeQueue();
    });
});