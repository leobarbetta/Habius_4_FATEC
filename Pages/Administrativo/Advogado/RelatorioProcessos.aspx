<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" CodeFile="RelatorioProcessos.aspx.cs" Inherits="Pages_Administrativo_Advogado_RelatorioProcessos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        $(window).resize(function () {
            drawChartTipoMovimentacao();
        });
    </script>
    <script>
        $(document).ready(function () {
            $("#txtInitialDate").mask("99/99/9999");
            $("#txtFinalDate").mask("99/99/9999");

        });
    </script>
    <link href="../../../css/tables.css" rel="stylesheet" />
    <asp:Literal ID="ltMovimentacao" runat="server" />
    <asp:Literal ID="ltClasses" runat="server" />
    <asp:Literal ID="ltPosicao" runat="server" />

    <h1 class="text-center page-header text" style="color: rgba(77, 77, 77, 0.90)">Relatório de Processos</h1>

    <div class="col-md-4">
        <div class="panel panel-default">
            <div class="panel-heading"><span class="glyphicon glyphicon-stats" style="padding-right: 5px" aria-hidden="true"></span>Movimentação</div>
            <div class="panel-body" style="height: 300px">
                <div style="width: 100%; height: 100%" id="piechart_TipoMovimentacao"></div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-default">
            <div class="panel-heading"><span class="glyphicon glyphicon-stats" style="padding-right: 5px" aria-hidden="true"></span>Classes</div>
            <div class="panel-body" style="height: 300px">
                <div style="width: 100%; height: 100%; margin-bottom: 0px" id="piechart_Classe"></div>
            </div>
        </div>      
    </div>
    <div class="col-md-4">
        <div class="panel panel-default">
            <div class="panel-heading"><span class="glyphicon glyphicon-stats" style="padding-right: 5px" aria-hidden="true"></span>Posição cliente</div>
            <div class="panel-body" style="height: 300px">
                <div style="width: 100%; height: 100%; margin-bottom: 0px" id="piechart_Posicao"></div>
            </div>
        </div>      
    </div>
    <div class="col-md-12">
        <div class="panel panel-default">
            <div class="panel-heading"><span class="glyphicon glyphicon-list-alt" style="padding-right: 5px" aria-hidden="true"></span>Histórico de Processos</div>
            <asp:UpdatePanel ID="updateMsg" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div runat="server" id="divMensagem">
                        <asp:Label ID="lblMensagem" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row">
                <div class="container-fluid">
                    <asp:Panel ID="pnl" runat="server" DefaultButton="btnBusca">
                        <div class="col-md-12 align-right" style="padding-top: 10px">
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
                    <asp:UpdatePanel ID="updateGrid" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="table-responsive col-md-12">
                                <asp:GridView ID="gdvProcesso" OnPageIndexChanging="gdvProcesso_PageIndexChanging"
                                    runat="server" PageSize="10" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" CssClass="table table-hover table-condensed"
                                    AutoGenerateColumns="False" CellPadding="6" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="NOME_CLIENTE" SortExpression="NOME_CLIENTE" HeaderText="Cliente" />
                                        <asp:BoundField DataField="NUMEROPROCESSO" SortExpression="NUMEROPROCESSO" HeaderText="Nº Processo" />                                        
                                        <asp:BoundField DataField="CLASSE" SortExpression="CLASSE" HeaderText="Classe" />
                                        <asp:BoundField DataField="POSICAO_CLIENTE" SortExpression="POSICAO_CLIENTE" HeaderText="Posição do Cliente" />
                                        <asp:BoundField DataField="MOVIMENTACAO" SortExpression="MOVIMENTACAO" HeaderText="Movimentação" />
                                        <asp:BoundField DataField="DATA_CRIACAO" SortExpression="DATA_CRIACAO" HeaderText="Data Cadastro" DataFormatString="{0:d}" />
                                    </Columns>
                                </asp:GridView>
                                <p class="text-primary text-right">
                                    <asp:Label ID="lblQtd" runat="server" />
                                </p>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gdvProcesso" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>



































</asp:Content>

