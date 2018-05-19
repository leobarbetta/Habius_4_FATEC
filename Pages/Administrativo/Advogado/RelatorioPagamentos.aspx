<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" CodeFile="RelatorioPagamentos.aspx.cs" Inherits="Pages_Administrativo_Advogado_RelatorioPagamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <asp:Literal ID="lt" runat="server"></asp:Literal>
    <asp:Literal ID="ltPagamentos" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../css/tables.css" rel="stylesheet" />
    <script src="../../../JavaScript/Mask.js"></script>

    <script type="text/javascript">
        $(window).resize(function () {
            drawChartServicoPagamentoByMonth();
            drawChartPagamentosAno();
        });
    </script>
    <script>
        $(document).ready(function () {
            $("#txtInitialDate").mask("99/99/9999");
            $("#txtFinalDate").mask("99/99/9999");
        });
    </script>
    <h1 class="text-center page-header text" style="color: rgba(77, 77, 77, 0.90)">Relatório de Pagamentos</h1>


    <div runat="server" id="divMensagem">
        <asp:Label ID="lblMensagem" runat="server" />
    </div>
    <asp:Panel ID="panel" runat="server" DefaultButton="btnBusca">
        <div class="col-md-12 align-right" style="padding-bottom: 30px">
            <div class="form-inline">
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group-addon">De</div>
                        <asp:TextBox ID="txtInitialDate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calendar1" runat="server" TargetControlID="txtInitialDate" CssClass="calendar" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group-addon">Até</div>
                        <asp:TextBox ID="txtFinalDate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calendar2" runat="server" TargetControlID="txtFinalDate" CssClass="calendar" />
                        <div class="input-group-btn">
                            <asp:LinkButton runat="server" ID="btnBusca" OnClick="btnBusca_Click" CssClass="btn btn-default"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <div class="row">
        <div class="col-md-6">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading"><span class="glyphicon glyphicon-stats" style="padding-right: 5px" aria-hidden="true"></span>Pagamentos no Ano</div>
                    <div class="panel-body" style="min-height: 400px">
                        <div style="height: 370px" id="chart_PagamentosAno"></div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading"><span class="glyphicon glyphicon-stats" style="padding-right: 5px" aria-hidden="true"></span>Pagamentos por Tipo</div>
                    <div class="panel-body" style="height: 400px">
                        <div class="col-md-10 col-md-offset-1">
                            <div style="height: 370px" id="piechart_ServicoPagamentoByMonth"></div>
                        </div>

                    </div>
                </div>
            </div>
        </div>


        <div class="col-md-6">

            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading"><span class="glyphicon glyphicon-list-alt" style="padding-right: 5px" aria-hidden="true"></span>Pagamentos</div>
                    <div class="panel-body" style="max-height: 860px">
                        <asp:UpdatePanel ID="updateGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gdvPagamento" OnPageIndexChanging="gdvPagamento_PageIndexChanging" AllowPaging="true" PageSize="10" runat="server" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" CssClass="table table-hover table-condensed"
                                    AutoGenerateColumns="False" CellPadding="6" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="PAG_VALOR" SortExpression="PAG_VALOR" HeaderText="Valor" DataFormatString="{0,19:C}" />
                                        <asp:BoundField DataField="PAG_DATAPAGAMENTO" SortExpression="PAG_DATAPAGAMENTO" HeaderText="Data" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="SEV_SERVICO" SortExpression="SEV_SERVICO" HeaderText="Tipo" />
                                    </Columns>
                                </asp:GridView>
                                <p class="text-primary text-right">
                                    <asp:Label ID="lblqtdPagamento" runat="server" />
                                </p>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gdvPagamento" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>


            <div class="col-md-5 pull-right">
                <blockquote class="block-success">
                    <div class="row" style="padding-left: 10px">
                        <h4><strong>VALOR TOTAL:</strong></h4>
                    </div>
                    <div class="row" style="text-align: right; padding-right: 30px">
                        <h3><strong>
                            <asp:Label ID="lblTotalPagamento" runat="server" /></strong></h3>
                    </div>
                </blockquote>
            </div>

        </div>
    </div>


</asp:Content>

