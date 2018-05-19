<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CadastrarGastos.aspx.cs" Inherits="Pages_Administrativo_Advogado_CadastrarGastos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../css/tables.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title text-center" style="font-size: xx-large"><span class="glyphicon glyphicon-list-alt" aria-hidden="true" style="padding-right: 10px"></span>Despesas do Escritório</h1>
                </div>
                <div class="panel-body" style="height: 500px">
                    <div class="col-md-12">
                        <div class="col-md-offset-11 align-right">
                            <asp:LinkButton ID="btnAddDespesa" runat="server" title="Adicionar Gasto" OnClick="btnAddDespesa_Click" CssClass="btn btn-success"><span class=" glyphicon glyphicon-plus"></span></asp:LinkButton>
                        </div>
                    </div>

                    <div class="col-md-12 table-responsive">
                        <asp:UpdatePanel runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="gdvDespesasEscritorio" runat="server" OnPageIndexChanging="gdvDespesasEscritorio_PageIndexChanging" AllowPaging="true"
                                    AutoGenerateColumns="false" PageSize="6" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center"
                                    CssClass="table table-hover table-condensed" CellPadding="6" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="DES_VALOR" HeaderText="Valor" DataFormatString="{0,19:C}" />
                                        <asp:BoundField DataField="DES_DATA" HeaderText="Data" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="TID_TIPODESPESA" HeaderText="Tipo" />
                                        <asp:BoundField DataField="DES_OBS" HeaderText="Observação" />
                                    </Columns>
                                </asp:GridView>
                                <p class="text-primary text-right">
                                    <asp:Label ID="lblqtdDespesa" runat="server" />
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <blockquote class="block-success">
                        <div class="row" style="padding-left: 10px">
                            <h4><strong>TOTAL:</strong></h4>
                        </div>
                        <div class="row" style="text-align: right; padding-right: 30px">
                            <h3><strong>
                                <asp:Label ID="lblValorTotalDespesa" runat="server" /></strong></h3>
                        </div>
                    </blockquote>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <asp:UpdatePanel ID="updateAddDespesas" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- label para rodar modal adicionar despesas -->
            <asp:Label ID="lblModalAddDespesas"
                runat="server"></asp:Label>
            <!-- fim -->
            <!-- MODAL ADICIONAR DESPESAS -->
            <ajaxToolkit:ModalPopupExtender ID="modalAddDespesas" runat="server" PopupControlID="panelAddDespesas" RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="modalFade" DropShadow="false" TargetControlID="lblModalAddDespesas">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelAddDespesas" runat="server">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:LinkButton ID="btnCalcelModalDespesas" OnClick="btnCalcelModalDespesas_Click" runat="server"><span style="color:#ff0000" class="close glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            <h2 class="modal-title text-center">Despesas</h2>
                        </div>
                        <div class="modal-body">                            
                            <div id="divmsgModalAddDespesaEscritorio" runat="server">
                                <asp:Label ID="lblmsgModalAddDespesaEscritorio" runat="server"></asp:Label>
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
                                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" />
                                    </div>
                                    <div class="form-group">
                                        <label style="font-size: small">Descrição:</label>
                                        <asp:TextBox ID="txtDescricaoDespesa" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Data:</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtData" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <asp:LinkButton runat="server" ID="btnCalendario"><span class=" glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                <ajaxToolkit:CalendarExtender CssClass="calendar" TargetControlID="txtData" ID="calendarExtender" PopupButtonID="btnCalendario" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label style="font-size: small">Observação:</label>
                                        <asp:TextBox ID="txtObs" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton ID="btnSalvar" OnClick="btnSalvar_Click" OnClientClick="return confirm('Deseja adicionar pagamento?');" CssClass="btn btn-primary" runat="server">Salvar</asp:LinkButton>
                            </div>
                        </div>
                    </div>
            </asp:Panel>
            <!-- FIM MODAL ADICIONAR DESPESAS-->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" />
            <asp:AsyncPostBackTrigger ControlID="btnCalcelModalDespesas" />
            <asp:AsyncPostBackTrigger ControlID="btnAddDespesa" />
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

