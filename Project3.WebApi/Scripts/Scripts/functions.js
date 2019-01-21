/**
 * Kiểm tra điều kiện xe có hợp lệ không để render lên giao diện
 * @param {any} data trạng thái của 1 xe có hợp lệ hay không
 */
function carItem(data) {
    var state = "";
    if (data.state === 0) {
        state = "normal";
    }
    else {
        state = "infringe";
    }
    var item = `<tr class="item ${state}" title="${data.carID}">
                    <td>${data.carID}</td>
                </tr>`;
    return item;
}

function studentItem(data) {
    var state = "unchoose";

    var item = `<tr class="item ${state}" title="${data.Name}">
                    <td>${data.ID}</td>
                </tr>`;
    return item;
}


/**
 * Cập nhật lại tọa độ đang theo dõi khi nhấn vào vị trí khác
 * */
function updateMonitoringPosition(id) {
    debugger
    var crCar = carList.find(x => x.carID === id);
    if (crCar) {
        var coordinate = stringToObject(crCar.coordinate);
        map.panTo(new google.maps.LatLng(coordinate.lat, coordinate.lng));
        $('.speed').text('Tốc độ: ' + crCar.speed + ' Km/h');
        $('.location').text('Vị trí: ' + crCar.location);
    }
}

/**
 * Khởi tạo google map api khi mới load trang
 * */
function initMapAPI() {

    //setLocationMap();

    var myLatLng = { lat: 0, lng: 0 };
    map = new google.maps.Map(document.getElementById('map'), {
        center: myLatLng,
        zoom: 8
    });

    //marker.setMap(map);
}

function setLocationMap() {
    document.getElementById('map').style.height = "585px";
    document.getElementById('map').style.width = "970px";
    document.getElementById('map').style.margin = "0px";
    document.getElementById('map').style.padding = "0px";
    document.getElementById('page-wrapper').style.marginLeft = "180px";
    document.getElementById('page-wrapper').style.padding = "0px";
}

function buildCurrentDate() {
    var date = new Date();
    return (date.getDate() < 10 ? "0" : "") + date.getDate()
        + "-" + ((date.getMonth() + 1) < 10 ? "0" : "") + (date.getMonth() + 1)
        + "-" + date.getFullYear();
}

function drawMark(data) {
    //debugger
    var coordinate = stringToObject(data.coordinate);
    var marker = new google.maps.Marker({
        map: map,
        label: data.carID,
        position: { lat: coordinate.lat, lng: coordinate.lng },
        icon: "../../Content/Icon/beachflag.png",
        title: data.carID
    });
    markers.push(marker);
}

function resetElement() {
    $('.list-cars').html('');
    removeMarker();
}

function removeMarker() {
    for (let marker of markers) {
        marker.setMap(null);
    }
    for (let line of lines) {
        line.setMap(null);
    }
    markers = [];
    lines = [];
}

function stringToObject(data) {
    return JSON.parse(data);
}

function requestUpdateMap() {
    var resuilt = true;
    $.ajax({
        method: 'GET',
        url: "/monitoring/getCurrentState",
        async: false,
        success: function(result) {
            resetElement();
            if (result.length > 0) {
                for (let data of result) {
                    $('.list-cars').append(carItem(data));
                    drawMark(data);
                }
                carList = result;
            }
        },
        error: function() {
            resuilt = false;
        }
    });
    return resuilt;
}

function getHistory() {
    var date = $('.date-time').combodate('getValue');
    if (isValidDate(date)) {
        getHistoryByStudentID(date);
    }
}

function chooseStudent(_this) {
    $('.item').removeClass('normal');
    $(_this).addClass('normal');

    var date = $('.date-time').combodate('getValue');

    if (isValidDate(date)) {
        getHistoryByStudentID(date);
    }
}

function isValidDate(date) {
    return moment(date).isValid();
}

function getHistoryByStudentID(date) {
    var day = 0;
    var month = 0;
    var year = 0;
    var id = '';
    var data = {};

    day = date.slice(0, 2);
    month = date.slice(3, 5);
    year = date.slice(6, 10);

    id = $('.normal').eq(0).text().trim();

    data = {
        studentID: id,
        day: day,
        month: month,
        year: year
    };
    debugger
    $.ajax({
        method: 'POST',
        url: "/history/getDataHistoryByStudentID",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(data),
        success: function(result) {
            removeMarker();
            if (result.length > 0) {
                createLine(result);
            }
        },
        error: function(e) {
        }
    });

    function createLine(dataArr) {
        if (dataArr.length > 0) {
            var flightPlanCoordinates = [];
            for (student of dataArr) {
                flightPlanCoordinates.push(stringToObject(student.coordinate));
            }
            var flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                geodesic: true,
                strokeColor: '#FF0000',
                strokeOpacity: 1.0,
                strokeWeight: 2
            });

            flightPath.setMap(map);
            map.panTo(new google.maps.LatLng(stringToObject(dataArr[0].coordinate).lat, stringToObject(dataArr[0].coordinate).lng));
            lines.push(flightPath);
        }
    }

}