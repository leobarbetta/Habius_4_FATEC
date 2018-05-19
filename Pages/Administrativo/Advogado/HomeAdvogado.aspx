<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" CodeFile="HomeAdvogado.aspx.cs" Inherits="Pages_Administrativo_Advogado_HomeAdvogado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../css/tables.css" rel="stylesheet" />
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <asp:Literal ID="ltProcesso" runat="server" />
    <asp:Literal ID="ltPagamentos" runat="server" />
    <asp:Literal ID="ltGastos" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .panel > .heading {
            background-color: #A9A9A9;
        }
    </style>

    <script type="text/javascript">
        $(window).resize(function () {
            drawChartTipoMovimentacaoAbertas();
            drawChartTipoGastosByMonth();
            drawChartServicoPagamentoByMonth();
        });
    </script>


    <div class="col-md-5">
        <div class="panel" data-role="panel">
            <div class="heading">
                <span class="title">Processos em Aberto</span>
                <asp:Label ID="lblTeste" runat="server" />
            </div>
            <div class="content" style="height: 250px">
                <div style="width: 100%; height: 100%" id="piechart_TipoMovimentacaoAbertas"></div>
            </div>
        </div>
        <div class="panel" data-role="panel">
            <div class="heading">
                <span class="title">Despesas do Mês</span>
            </div>
            <div class="content" style="height: 250px">
                <div style="width: 100%; height: 100%" id="piechart_TipoGastosByMonth"></div>
            </div>
        </div>
        <div class="panel" data-role="panel">
            <div class="heading">
                <span class="title">Pagamentos do Mês</span>
            </div>
            <div class="content" style="height: 250px">
                <div style="width: 100%; height: 100%" id="piechart_ServicoPagamentoByMonth"></div>
            </div>
        </div>
    </div>

    <div class="col-md-7">
        <div class="panel collapse" data-role="panel">
            <div class="heading">
                <span class="title">Processos</span>
            </div>
            <div class="content table-responsive" style="height: 572px">
                <asp:UpdatePanel ID="updateProcesso" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView runat="server" ID="gdvProcesso" OnPageIndexChanging="gdvProcesso_PageIndexChanging" AllowPaging="true" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnRowDataBound="gdvProcesso_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="NUMEROPROCESSO" SortExpression="NUMEROPROCESSO" HeaderText="Processo Nº" />
                                <asp:BoundField DataField="ASSUNTO" SortExpression="ASSUNTO" HeaderText="Assunto" />
                                <asp:BoundField DataField="NOME_CLIENTE" SortExpression="NOME_CLIENTE" HeaderText="Cliente" />
                                <asp:BoundField DataField="DATA_PROCESSO" SortExpression="DATA_PROCESSO" DataFormatString="{0:g}" HeaderText="Data Audiencia" />
                            </Columns>
                            <EditRowStyle HorizontalAlign="Center"></EditRowStyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <RowStyle HorizontalAlign="Center"></RowStyle>
                        </asp:GridView>
                        <p class="text-primary small">
                            <asp:Label ID="lbl" runat="server" />
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!--
        <div class="panel collapsed" data-role="panel">
            <div class="heading">
                <span class="title">Calendario</span>
            </div>
            <div class="content">
                <div class="panel-body" style="height: 400px">
                    <div class="navy calendar" data-role="calendar">
                        <div class="calendar-grid">
                            <asp:UpdatePanel runat="server" ID="update2">
                                <ContentTemplate>
                                    <asp:Calendar ID="calendar" runat="server" CssClass=" calendar"
                                        FirstDayOfWeek="Sunday" NextPrevFormat="FullMonth" BackColor="White" BorderColor="White" BorderWidth="5px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="100%" BorderStyle="Solid" DayHeaderStyle-HorizontalAlign="Center" DayHeaderStyle-VerticalAlign="Middle">
                                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" BorderColor="White" VerticalAlign="Middle" BackColor="White" />
                                        <DayStyle BorderColor="Silver" BorderWidth="1px" Font-Overline="False" Font-Strikeout="False" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                        <OtherMonthDayStyle ForeColor="Gray" />
                                        <SelectedDayStyle BackColor="#337AB7" ForeColor="White" />
                                        <TitleStyle BackColor="White" BorderWidth="3px" Font-Bold="True" Font-Size="12pt" ForeColor="#666666" HorizontalAlign="Center" />
                                        <TodayDayStyle BackColor="#CCCCCC" />
                                        <WeekendDayStyle ForeColor="Red" />
                                    </asp:Calendar>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>
        </div>-->
    </div>

</asp:Content>

