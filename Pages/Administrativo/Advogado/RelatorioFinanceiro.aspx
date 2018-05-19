<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" CodeFile="RelatorioFinanceiro.aspx.cs" Inherits="Pages_Administrativo_Advogado_RelatorioFinanceiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    

    
    <asp:Literal ID="lt" runat="server" />


   
    <h1 class="text-center page-header text" style="color: rgba(77, 77, 77, 0.90)">Relatório Financeiro</h1>

    

    

    <div class="row">
        <div class="col-md-3 col-md-offset-3">
            <blockquote class="block-danger">
                <div class="row" style="padding-left: 10px">
                    <h4><strong>DESPESAS:</strong></h4>
                </div>
                <div class="row" style="text-align: right; padding-right: 30px">
                    <h3><strong>
                        <asp:Label ID="lblTotalDespesa" runat="server" /></strong></h3>
                </div>
            </blockquote>
        </div>
        <div class="col-md-3">
            <blockquote class=" block-primary">
                <div class="row" style="padding-left: 10px">
                    <h4><strong>PAGAMENTOS:</strong></h4>
                </div>
                <div class="row" style="text-align: right; padding-right: 30px">
                    <h3><strong>
                        <asp:Label ID="lblTotalPagamento" runat="server" /></strong></h3>
                </div>
            </blockquote>
        </div>
        <div class="col-md-3">
            <blockquote class="block-success">
                <div class="row" style="padding-left: 10px">
                    <h4><strong>LUCRO:</strong></h4>
                </div>
                <div class="row" style="text-align: right; padding-right: 30px">
                    <h3><strong>
                        <asp:Label ID="lblFaturamento" runat="server" /></strong></h3>
                </div>
            </blockquote>
        </div>
    </div>

    <div class="row" style="padding-bottom:30px">
        <div class="col-md-10 col-md-offset-1">
            <div style="width:100%; height:500px" id="chart_Financeiro" "></div>
        </div>
    </div>
    

    <script type="text/javascript">
        $(window).resize(function () {
            drawChartFinanceiro();

        });
    </script>


</asp:Content>

