
$(document).ready(function () {

    //--初始化图表
    function initPeityElements() {
        if (!jQuery().peity) {
            return;
        }

        $(".stat.bad .line-chart").peity("line", {
            height: 20,
            width: 50,
            colour: "#d12610",
            strokeColour: "#666"
        }).show();

        $(".stat.bad .bar-chart").peity("bar", {
            height: 20,
            width: 50,
            colour: "#d12610",
            strokeColour: "#666"
        }).show();

        $(".stat.ok .line-chart").peity("line", {
            height: 20,
            width: 50,
            colour: "#37b7f3",
            strokeColour: "#757575"
        }).show();

        $(".stat.ok .bar-chart").peity("bar", {
            height: 20,
            width: 50,
            colour: "#37b7f3"
        }).show();

        $(".stat.good .line-chart").peity("line", {
            height: 20,
            width: 50,
            colour: "#52e136"
        }).show();

        $(".stat.good .bar-chart").peity("bar", {
            height: 20,
            width: 50,
            colour: "#52e136"
        }).show();

        //
        $(".stat.bad.huge .line-chart").peity("line", {
            height: 20,
            width: 40,
            colour: "#d12610",
            strokeColour: "#666"
        }).show();

        $(".stat.bad.huge .bar-chart").peity("bar", {
            height: 20,
            width: 40,
            colour: "#d12610",
            strokeColour: "#666"
        }).show();

        $(".stat.ok.huge .line-chart").peity("line", {
            height: 20,
            width: 40,
            colour: "#37b7f3",
            strokeColour: "#757575"
        }).show();

        $(".stat.ok.huge .bar-chart").peity("bar", {
            height: 20,
            width: 40,
            colour: "#37b7f3"
        }).show();

        $(".stat.good.huge .line-chart").peity("line", {
            height: 20,
            width: 40,
            colour: "#52e136"
        }).show();

        $(".stat.good.huge .bar-chart").peity("bar", {
            height: 20,
            width: 40,
            colour: "#52e136"
        }).show();

    };

    //--服务调用情况分析图表
    function chartServiceInvoke(chartData) {
        var nowTime = chartData.nowTime;

        if (chartData.data.length == 0) return;

        var plot = $.plot($("#chart_2"), chartData.data, {
            series: {
                lines: {
                    show: true,
                    lineWidth: 2,
                    fill: true,
                    fillColor: {
                        colors: [{
                            opacity: 0.05
                        }, {
                            opacity: 0.01
                        }
                        ]
                    }
                },
                points: {
                    show: true,
                    radius: 3,
                    lineWidth: 1
                },
                shadowSize: 2
            },
            grid: {
                hoverable: true,
                clickable: true,
                tickColor: "#eee",
                borderColor: "#eee",
                borderWidth: 1
            },
            colors: ["#d12610", "#37b7f3", "#52e136"],
            xaxis: {
                ticks: 11,
                tickDecimals: 0,
                tickFormatter: timeFormatter,
                tickColor: "#eee",
            },
            yaxis: {
                ticks: 11,
                tickDecimals: 0,
                tickColor: "#eee",
            }
        });

        function timeFormatter(axis) {
            var hour = parseInt(nowTime.split(':')[0]);
            var minute = parseInt(nowTime.split(':')[1]);
            var lastMin = 30 - axis;

            minute = minute - lastMin;
            if (minute < 0) {
                minute += 60;
                hour -= 1;
            }

            if (minute < 10) minute = "0" + minute;
            if (hour < 10) hour = "0" + hour;

            return hour + ":" + minute;
        };


        function showTooltip(x, y, contents) {
            $('<div id="tooltip">' + contents + '</div>').css({
                position: 'absolute',
                display: 'none',
                top: y + 5,
                left: x + 15,
                border: '1px solid #333',
                padding: '4px',
                color: '#fff',
                'border-radius': '3px',
                'background-color': '#333',
                opacity: 0.80
            }).appendTo("body").fadeIn(200);
        }

        var previousPoint = null;
        $("#chart_2").bind("plothover", function (event, pos, item) {
            $("#x").text(pos.x.toFixed(2));
            $("#y").text(pos.y.toFixed(2));

            if (item) {
                if (previousPoint != item.dataIndex) {
                    previousPoint = item.dataIndex;

                    $("#tooltip").remove();
                    var x = timeFormatter(item.datapoint[0]),
                        y = item.datapoint[1] + "ms";

                    if (item.datapoint[1] > 0)
                        console.log(item.datapoint);
                    var content = item.series.label + "（" + x + "）<br> 最长延时：" + y
                        + "<br>>100ms：" + item.datapoint[2];
                    showTooltip(item.pageX, item.pageY, content);
                }
            } else {
                $("#tooltip").remove();
                previousPoint = null;
            }
        });
    };


    //--初始化Overview图表
    initPeityElements();

    //--加载图表数据
    $.ajax({
        url: "Index.aspx",
        async: true,
        dataType: 'json',
        data: "Action=GetChartData&BusinessID=" + BusinessID,
        success: function (msg) {
            chartServiceInvoke(msg);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });

    //--定时刷新画面
    setInterval(function () {
        $.ajax({
            url: "Index.aspx",
            async: true,
            dataType: 'json',
            data: "Action=GetChartData&BusinessID=" + BusinessID,
            success: function (msg) {
                chartServiceInvoke(msg);
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    }, 1000 * 60);

});