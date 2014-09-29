//--demo.js

$(document).ready(function () {
    function Invoke() {
        var methodType = $("#selMethodType").val();
        var dataType = $("#selDataType").val();
        var methodName = $("#txtMethodName").val();
        var serviceName = $("#txtServiceName").val();
        var message = $("#txtRequest").val();

        methodName = methodType + ":" + dataType + ":" + methodName;

        $.esb.invoke(serviceName, methodName, message, function (response) {

            $("#txtResponse").val(response);
        });

    };

    function InvokeQueue() {
        alert("InvokeQueue");
    };

    $("#btnInvoke").click(function () {
        Invoke();
    });

    $("#btnInvokeQueue").click(function () {
        InvokeQueue();
    });
});