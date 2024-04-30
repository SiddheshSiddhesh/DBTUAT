<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenStreetMap.aspx.cs" Inherits="DBTPoCRA.OpenStreetMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POCRA MAP</title>
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css"
        integrity="sha256-p4NxAoJBhIIN+hmNHrzRCf9tD/miZyoHS5obTRR9BMY="
        crossorigin="" />

    <!-- Make sure you put this AFTER Leaflet's CSS -->
    <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"
        integrity="sha256-20nQCchB9co0qIjJZRGuk2/Z9VM+kNiyxNV1lvTlZBo="
        crossorigin=""></script>

    <script src="assets/js/leaflet-tilelayer-here.js"></script>
    <%--<script src="https://ivansanchez.gitlab.io/Leaflet.TileLayer.HERE/leaflet-tilelayer-here.js"></script>--%>

    <style>
        #map {
            height: 100vh;
        }

        .leaflet-control-attribution{
            display:none;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

      <%--  <span>Map Location</span>--%>
        <div>
            <div id="map"></div>
        </div>
    </form>


    <!-- ol-layerswitcher -->





    <script>


        var a = 0;
        var b = 0;
        var url = location.search;
        var qs = url.substring(url.indexOf('?') + 1).split('&');
        for (var i = 0; i < qs.length; i++) {
            var m = qs[i].split('=');
            if (m[0] == "A") {
                a = m[1];
            }
            if (m[0] == "B") {
                b = m[1];
            }
        }
         
        var map = L.map('map').setView([a, b], 13); 

        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">POCRA</a>'
        }).addTo(map);

        var marker = L.marker([a, b]).addTo(map);

        var circle = L.circle([a, b], {
            color: 'red',
            fillColor: '#f03',
            fillOpacity: 0.5,
            radius: 50
        }).addTo(map);

        var popup = L.popup()
            .setLatLng([a, b])
            .setContent("Activity Location <br/><b>Latitude :</b>" + a +"<br/><b>Longitude :</b>"+b)
            .openOn(map);

         
        var schemes = [
            'satellite.day',
            'hybrid.day.transit',
            //'hybrid.grey.day',
            //'hybrid.day.mobile',
            //'hybrid.day',
            //'normal.day',
            //'normal.day.grey',
            'normal.day.transit'
            //'normal.night.transit',
            // 		'normal.traffic.day',
            // 		'normal.traffic.night',
            //'normal.day.custom',
            //'normal.night',
            //'normal.night.grey',
            //'pedestrian.day',
            //'pedestrian.day.mobile',
            //'pedestrian.night',
            //'pedestrian.night.mobile',
            //'carnav.day.grey',
            //'normal.day.mobile',
            //'normal.day.grey.mobile',
            //'normal.day.transit.mobile',
            //'normal.night.transit.mobile',
            //'normal.night.mobile',
            //'normal.night.grey.mobile',
            //'reduced.day',
            //'reduced.night',
            //'terrain.day',                               
            //'terrain.day.mobile',
            
        ]

        var baselayers = {};

        for (var i in schemes) {
            var scheme = schemes[i];
            baselayers[scheme] = L.tileLayer.here({
                appId: 'itFu46QqjFn8CY0rfLdB',
                appCode: 'fUSfSpQI48RWqR4PxOu7Kg',
                scheme: scheme
            });
        }

        baselayers['satellite.day'].addTo(map);

        L.control.layers(baselayers, {}, { collapsed: false }).addTo(map)


        $(".leaflet-control").hide();

    </script>

</body>
</html>
