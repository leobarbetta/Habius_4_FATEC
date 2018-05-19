<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestesLeo.aspx.cs" Inherits="Pages_Administrativo_Advogado_TestesLeo" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Literal ID="lt" runat="server"></asp:Literal>
</head>

<body>
    <form id="form1" runat="server">
       
        <script type="text/javascript">
            $(window).resize(function () {
                drawChart();
            });
    </script>

        <script type="text/javascript"> google.load( "visualization", "1", {packages:["corechart"]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = google.visualization.arrayToDataTable([
             ['2015', 'Despesas', 'teste'],
        ['janeiro', 0, 10],
        ['fevereiro', 0, 100],
        ['março', 0, 1000],
        ['abril', 31, 1000],
        ['maio', 30, 1000],
        ['junho', 0, 1000],
        ['julho', 0, 10],
        ['agosto', 0, 10],
        ['setembro', 1000, 1000],
        ['outubro', 2111, 1000],
        ['novembro', 1185, 1000],
        ['dezembro', 0, 100],
        ]);
        var options = {
            backgroundColor: '#fff', chart: { title: 'Relatorio Gastos', }

        }; var chart = new google.visualization.ColumnChart(document.getElementById('chart_div')); chart.draw(data, options);
         }</script>



        <div id="chart_div"></div>
        <asp:Label ID="lblTeste" runat="server" />
    </form>
</body>
</html>
