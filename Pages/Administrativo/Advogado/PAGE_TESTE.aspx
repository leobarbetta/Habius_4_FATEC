<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" CodeFile="PAGE_TESTE.aspx.cs" Inherits="Pages_Administrativo_Advogado_PAGE_TESTE" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Load the AJAX API-->
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">

        // Load the Visualization API and the piechart package.
        google.load('visualization', '1.0', { 'packages': ['corechart'] });

        // Set a callback to run when the Google Visualization API is loaded.
        google.setOnLoadCallback(drawChart);

        // Callback that creates and populates a data table,
        // instantiates the pie chart, passes in the data and
        // draws it.
        function drawChart() {

            // Create the data table.
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Topping');
            data.addColumn('number', 'Slices');
            data.addRows([
              ['Mushrooms', 3],
              ['Onions', 1],
              ['Olives', 1],
              ['Zucchini', 1],
              ['Pepperoni', 2]
            ]);

            // Set chart options
            var options = {
                'title': 'How Much Pizza I Ate Last Night',
                'backgroundColor': 'none',
                'chartArea': { left: '100', width: '100%', height: '100%' },


            };

            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
            chart.draw(data, options);
        }
    </script>

    <!--Div that will hold the pie chart-->


    <div class="col-md-8 col-md-offset-2" style="background-color: red; height:600px">
        <div id="chart_div" style="width: 100%; height: 100%;"></div>
    </div>

    <script type="text/javascript">
        $(window).resize(function () {
            drawChart();
   
        });
    </script>







</asp:Content>

