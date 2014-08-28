/*
 *  mb-esb.js 
 *  Tony 2014.8.19
 *  ESB AJAX调用类
 */

jQuery.esb = {
    /*
     * 版本号
     */
    version: '1.0.0',
    /*
     * 调用中心地址
     */
    baseurl: 'http://192.168.56.1/CallCenter/',
    /*
     *  请求响应调用 JSONP
     */
    invoke: function (serviceName, methodName, message, fn) {
        var response;
        var invokeUrl = jQuery.esb.baseurl + "ESB_InvokeService.ashx?callback=?";
        $.ajax({
            url: invokeUrl,
            async: false,
            crossDomain: true,
            dataType: "jsonp",
            data: "ServiceName=" + serviceName + "&MethodName=" + methodName + "&Message=" + message,
            success: function (msg) {
                response = msg.message;
                fn(response);
            }
        });
    },
    /*
     *  请求响应调用 POST
     */
    invokePost: function (serviceName, methodName, message, fn) {
        var response;
        var invokeUrl = "ESB_InvokeService.ashx";
        $.ajax({
            url: invokeUrl,
            type: "POST",
            data: "ServiceName=" + serviceName + "&MethodName=" + methodName + "&Message=" + message,
            success: function (msg) {
                fn(msg);
            }
        });
    }
}