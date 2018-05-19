<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CadastraPagamentoCliente.aspx.cs" Inherits="Pages_Administrativo_Advogado_CadastraPagamentoCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../css/tables.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title text-center" style="font-size: xx-large"><span class="glyphicon glyphicon-list-alt" aria-hidden="true" style="padding-right: 10px"></span>Pagamentos</h1>
                </div>
                <div class="panel-body" style="max-height: 400px">
                    <div class="col-md-12">
                        <div class="col-md-9">
                            <div class="form-inline">
                                <div class="form-group">
                                    <label class="control-label">Cliente:</label>
                                    <p class="form-control-static">
                                        <asp:Label CssClass="control-label" ID="lblNome" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-offset-11 align-right">
                            <asp:LinkButton ID="btnAbrirModal" runat="server" title="Adicionar Despesa" CssClass="btn btn-success" OnClick="btnAbrirModal_Click"><span class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                        </div>
                    </div>

                    <div class="col-md-12 table-responsive">
                        <asp:UpdatePanel ID="UpdateGrid" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="gridPagamento" runat="server" PageSize="6" OnPageIndexChanging="gridPagamento_PageIndexChanging" AllowPaging="true" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" CssClass="table table-hover table-condensed"
                                    AutoGenerateColumns="False" CellPadding="6" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="PAG_VALOR" HeaderText="Valor" DataFormatString="{0,19:C}" />
                                        <asp:BoundField DataField="PAG_DATAPAGAMENTO" HeaderText="Data" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="SEV_SERVICO" HeaderText="Servico" />
                                        <asp:BoundField DataField="PRO_NUMEROPROCESSO" HeaderText="Processo" />
                                    </Columns>
                                </asp:GridView>
                                <p class="text-primary text-right">
                                    <asp:Label ID="lblqtdPagamentoprocesso" runat="server" />
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 col-md-offset-8">
            <asp:UpdatePanel ID="updateTopo" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <blockquote class="block-success">
                        <div class="row" style="padding-left: 10px">
                            <h4><strong>VALOR TOTAL:</strong></h4>
                        </div>
                        <div class="row" style="text-align: right; padding-right: 30px">
                            <h3><strong>
                                <asp:Label ID="lblValorTotalPagamento" runat="server" /></strong></h3>
                        </div>
                    </blockquote>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>




    <asp:UpdatePanel ID="updateAddPagamento" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- label para rodar modal adicionar despesas -->
            <asp:Label ID="lblModalAddPagamento" runat="server" />
            <!-- fim -->
            <!-- MODAL ADICIONAR DESPESAS -->
            <ajaxToolkit:ModalPopupExtender ID="modalAddPagamento" runat="server" PopupControlID="panelAddPagamento" RepositionMode="RepositionOnWindowResizeAndScroll"
                BackgroundCssClass="modalFade" DropShadow="false" TargetControlID="lblModalAddPagamento">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelAddPagamento" runat="server">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:LinkButton ID="btnCalcelModalAddPagamento" OnClick="btnCalcelModalAddPagamento_Click" runat="server"><span style="color:#ff0000" class="close glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            <h2 class="modal-title text-center">Adicionar Pagamento</h2>
                        </div>
                        <div class="modal-body">
                            <div id="divMsgAddPagamento" runat="server">
                                <asp:Label ID="lblMsgAddPagamento" runat="server"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                       <form class="form-inline">
                                        <div class="form-group">
                                            <label style="font-size: small">Valor:</label>
                                            <div class="input-group">
                                                <div class="input-group-addon">R$</div>
                                                <asp:TextBox ID="txtValor" onKeyPress="return(MascaraMoeda(this,'.',',',event))" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </form>
                                    <div class="form-group">
                                        <label style="font-size: small">Tipo:</label>
                                        <asp:DropDownList ID="ddlServico" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlServico_SelectedIndexChanged" />
                                    </div>
                                    <div class="form-group">
                                        <label style="font-size: small">Descrição:</label>
                                        <asp:TextBox ID="txtDescricaoServico" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Data:</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDataPagamento" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <asp:LinkButton runat="server" ID="btnCalendario"><span class=" glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                <ajaxToolkit:CalendarExtender CssClass="calendar" TargetControlID="txtDataPagamento" ID="calendarExtender" PopupButtonID="btnCalendario" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label style="font-size: small">Processo:</label>
                                        <asp:DropDownList ID="ddlProcesso" runat="server" CssClass="form-control" Enabled="false" />
                                    </div>
                                </div>
                            </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnAddPagamento" CssClass="btn btn-primary" runat="server" OnClientClick="return confirm('Deseja adicionar pagamento?');" OnClick="btnAddPagamento_Click">Adicionar</asp:LinkButton>
                        </div>
                    </div>
                </div>
                </div>

            </asp:Panel>
            <!-- FIM MODAL ADICIONAR DESPESAS-->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCalcelModalAddPagamento" />
            <asp:AsyncPostBackTrigger ControlID="btnAddPagamento" />
        </Triggers>
    </asp:UpdatePanel>
     <script>
         function MascaraMoeda(objTextBox, SeparadorMilesimo, SeparadorDecimal, e) {
             var sep = 0;
             var key = '';
             var i = j = 0;
             var len = len2 = 0;
             var strCheck = '0123456789';
             var aux = aux2 = '';
             var whichCode = (window.Event) ? e.which : e.keyCode;
             if (whichCode == 13) return true;
             key = String.fromCharCode(whichCode); // Valor para o código da Chave
             if (strCheck.indexOf(key) == -1) return false; // Chave inválida
             len = objTextBox.value.length;
             for (i = 0; i < len; i++)
                 if ((objTextBox.value.charAt(i) != '0') && (objTextBox.value.charAt(i) != SeparadorDecimal)) break;
             aux = '';
             for (; i < len; i++)
                 if (strCheck.indexOf(objTextBox.value.charAt(i)) != -1) aux += objTextBox.value.charAt(i);
             aux += key;
             len = aux.length;
             if (len == 0) objTextBox.value = '';
             if (len == 1) objTextBox.value = '0' + SeparadorDecimal + '0' + aux;
             if (len == 2) objTextBox.value = '0' + SeparadorDecimal + aux;
             if (len > 2) {
                 aux2 = '';
                 for (j = 0, i = len - 3; i >= 0; i--) {
                     if (j == 3) {
                         aux2 += SeparadorMilesimo;
                         j = 0;
                     }
                     aux2 += aux.charAt(i);
                     j++;
                 }
                 objTextBox.value = '';
                 len2 = aux2.length;
                 for (i = len2 - 1; i >= 0; i--)
                     objTextBox.value += aux2.charAt(i);
                 objTextBox.value += SeparadorDecimal + aux.substr(len - 2, len);
             }
             return false;
         }

    </script>
</asp:Content>

