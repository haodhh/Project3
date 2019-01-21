var map;
var myLatLng = { lat: -34.397, lng: 150.644 };
var x = Math.random();
var y = Math.random();
var carList = {};
var markers = [];
var lines = [];

var studentObj = [
    {
        studentID: "1F9122BE-BD86-48F6-B34F-5011AAB0DC14",
        studentName: "ahihi"
    },
    {
        studentID: "1F9122BE-BD86-48F6-B34F-5011AAB0DC14",
        studentName: "ahihi"
    },
    {
        studentID: "1F9122BE-BD86-48F6-B34F-5011AAB0DC14",
        studentName: "ahihi"
    },
    {
        studentID: "1F9122BE-BD86-48F6-B34F-5011AAB0DC14",
        studentName: "ahihi"
    },
    {
        studentID: "1F9122BE-BD86-48F6-B34F-5011AAB0DC14",
        studentName: "ahihi"
    },
    {
        studentID: "1F9122BE-BD86-48F6-B34F-5011AAB0DC14",
        studentName: "ahihi"
    },
    {
        studentID: "1F9122BE-BD86-48F6-B34F-5011AAB0DC14",
        studentName: "ahihi"
    },
    {
        studentID: "1F9122BE-BD86-48F6-B34F-5011AAB0DC14",
        studentName: "ahihi"
    },
    {
        studentID: "1F9122BE-BD86-48F6-B34F-5011AAB0DC14",
        studentName: "ahihi"
    }
];

var eventMap = Object.create({
    initMap: function () {
        initMapAPI();
    },

    changeTargetMonitoring_OnClick: function (e) {
        id = this.textContent.trim();
        debugger
        if (id !== '') {
            if (typeMap === "Monitoring") {
                updateMonitoringPosition(id);
            }
            else if (typeMap === "History") {
                debugger
                chooseStudent(this);
            }
        }
    }
});

var eventWindow = Object.create({
    onLoad: function (e) {
        //debugger
        if (typeMap === "Monitoring") {
            $('.title-list-cars').text('Danh sách xe');

            $('.history-search').hide();
            debugger
            if (requestUpdateMap()) {
                if (carList.length > 0) {
                    var coordinate = stringToObject(carList[0].coordinate);
                    map.panTo(new google.maps.LatLng(coordinate.lat, coordinate.lng));
                }
            }

            var worker = new Worker("../Scripts/Scripts/workers.js");
            worker.postMessage(1);
            worker.onmessage = function (e) {
                requestUpdateMap();
            };
        }
        else {
            $('.history-search').show();
            $('.title-list-cars').text('Danh sách học viên');
            $.ajax({
                method: 'GET',
                url: "/api/getstudent",
                async: false,
                success: function (result) {
                    resetElement();
                    if (result.length > 0) {
                        for (let data of result) {
                            $('.list-cars').append(studentItem(data));
                        }
                    }
                },
                error: function () {
                    resuilt = false;
                }
            });
        }

        //$.ajax({
        //    method: 'POST',
        //    url: "/history/writeDataHistory?carID=a444f87e-ef46-47b8-96ac-4683a88d22c2",
        //    contentType: "application/json",
        //    dataType: "json",
        //    success: function (result) {
        //        debugger
        //    },
        //    error: function (e) {
        //        debugger
        //    }
        //});
    }
});

/**
 * Object xử lý sự kiện liên quan đến lịch sử
 * */
var history = Object.create({
    getHistory_OnClick: function (e) {
        getHistory();
    }
});