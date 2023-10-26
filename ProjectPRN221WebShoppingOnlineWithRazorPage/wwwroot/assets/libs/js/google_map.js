$(function () {


    //******************************************//
    // Markers
    //******************************************//
    var map_2;
    map_2 = new GMaps({
        div: '#map_2',
        lat: -12.043333,
        lng: -77.028333
    });
    map_2.addMarker({
        lat: -12.043333,
        lng: -77.03,
        title: 'Lima',
        details: {
            database_id: 42,
            author: 'HPNeo'
        },
        click: function (e) {
            if (console.log)
                console.log(e);
            alert('You clicked in this marker');
        }
    });
    map_2.addMarker({
        lat: -12.042,
        lng: -77.028333,
        title: 'Marker with InfoWindow',
        infoWindow: {
            content: '<p>HTML Content</p>'
        }
    });

});