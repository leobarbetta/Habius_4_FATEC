<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CadastraCustoProcesso.aspx.cs" Inherits="Pages_Administrativo_Advogado_CadastraCustoProcesso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../css/tables.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title text-center" style="font-size: xx-large"><span class="glyphicon glyphicon-list-alt" aria-hidden="true" style="padding-right: 10px"></span>Custos de Processo</h1>
                </div>

                <div class="panel-body" style="height: 500px">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="form-inline">
                                <div class="form-group">
                                    <label class="control-label">Processo:</label>
                                    <p class="form-control-static">
                                        <asp:Label CssClass="control-label" ID="lblProcesso" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-offset-11 align-right">
                            <asp:LinkButton ID="btnAddCustoProcesso" runat="server" title="Adicionar Despesa" CssClass="btn btn-success" OnClick="btnAddCustoProcesso_Click"><span class=" glyphicon glyphicon-plus"></span></asp:LinkButton>
                        </div>
                    </div>

                    <div class="col-md-12 table-responsive">
                        <asp:UpdatePanel runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="gridCustos" runat="server" OnRowCommand="gridCustos_RowCommand" AllowPaging="true" OnPageIndexChanging="gridCustos_PageIndexChanging" PageSize="6" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" CssClass="table table-hover table-condensed"
                                    AutoGenerateColumns="False" CellPadding="6" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="DES_VALOR" HeaderText="Valor" DataFormatString="{0,19:C}" />
                                        <asp:BoundField DataField="TID_TIPODESPESA" HeaderText="Categoria" />
                                        <asp:BoundField DataField="DES_OBS" HeaderText="Observação" />
                                        <asp:BoundField DataField="DES_DATA" HeaderText="Data" DataFormatString="{0:d}" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditarCusto" runat="server" title="Editar Custo" CommandName="EditarCusto" CommandArgument='<% #Bind("DES_CODIGO")%>'><span style="color:#1cb549" class="glyphicon glyphicon-pencil" aria-hidden="true"></span></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <p class="text-primary text-right">
                                    <asp:Label ID="lblqtddespesaprocesso" runat="server" />
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
                    <blockquote class="block-danger">
                        <div class="row" style="padding-left: 10px">
                            <h4><strong>VALOR TOTAL:</strong></h4>
                        </div>
                        <div class="row" style="text-align: right; padding-right: 30px">
                            <h3><strong>
                                <asp:Label ID="lblValorTotalCusto" runat="server" /></strong></h3>
                        </div>
                    </blockquote>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdateEditarCusto" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- label para rodar modal editar despesas -->
            <asp:Label ID="lblModalEditarCusto"
                runat="server"></asp:Label>
            <!-- fim -->
            <!-- MODAL EDITAR DESPESAS -->
            <ajaxToolkit:ModalPopupExtender ID="modalEditarCusto" runat="server" PopupControlID="panelEditarCusto" RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="modalFade" DropShadow="false" TargetControlID="lblModalEditarCusto">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelEditarCusto" runat="server">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:LinkButton ID="btnCancelarEditar" OnClick="btnCancelarEditar_Click" runat="server"><span style="color:#ff0000" class="close glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            <h2 class="modal-title text-center">Editar Custo</h2>
                        </div>
                        <div class="modal-body">
                            <div id="divMsgEditarCusto" runat="server">
                                <asp:Label ID="lblMsgEditarCusto" runat="server"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <form class="form-inline">
                                        <div class="form-group">
                                            <label style="font-size: small">Valor:</label>
                                            <div class="input-group">
                                                <div class="input-group-addon">R$</div>
                                                <asp:TextBox ID="txtEditarValor" ClientIDMode="Static" onKeyPress="return(MascaraMoeda(this,'.',',',event))" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </form>
                                    <div class="form-group">
                                        <label style="font-size: small">Tipo:</label>
                                        <asp:DropDownList ID="ddlEditarTipo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEditarTipo_SelectedIndexChanged" />
                                    </div>
                                    <div class="form-group">
                                        <label style="font-size: small">Tipo de Despesa:</label>
                                        <asp:TextBox ID="txtEditarDescricao" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Data:</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtEditarData" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <asp:LinkButton runat="server" ID="btnCalendar2"><span class=" glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                <ajaxToolkit:CalendarExtender CssClass="calendar" TargetControlID="txtEditarData" ID="calendarExtender1" PopupButtonID="btnCalendar2" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label style="font-size: small">Observação:</label>
                                        <asp:TextBox ID="txtEditarObs" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton ID="btnSalvarEditarCusto" CssClass="btn btn-primary" OnClientClick="return confirm('Deseja editar esse custo?');" runat="server" OnClick="btnSalvarEditarCusto_Click">Atualizar</asp:LinkButton>
                            </div>
                        </div>
                    </div>
            </asp:Panel>
            <!-- FIM MODAL EDITAR DESPESAS-->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSalvarEditarCusto" />
            <asp:AsyncPostBackTrigger ControlID="ddlEditarTipo" />
            <asp:AsyncPostBackTrigger ControlID="gridCustos" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="updateAddCusto" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <!-- label para rodar modal adicionar despesas -->
            <asp:Label ID="lblModalAddCusto"
                runat="server"></asp:Label>
            <!-- fim -->
            <!-- MODAL ADICIONAR DESPESAS -->
            <ajaxToolkit:ModalPopupExtender ID="modalAddCusto" runat="server" PopupControlID="panelAddCusto" RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="modalFade" DropShadow="false" TargetControlID="lblModalAddCusto">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelAddCusto" runat="server">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:LinkButton ID="btnCalcelModalCusto" OnClick="btnCalcelModalCusto_Click" runat="server"><span style="color:#ff0000" class="close glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            <h2 class="modal-title text-center">Adicionar Custo</h2>
                        </div>
                        <div class="modal-body">
                            <div id="divMsgAddCusto" runat="server">
                                <asp:Label ID="lblMsgAddCusto" runat="server"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Valor:</label>
                                        <div class="input-group">
                                            <div class="input-group-addon">R$</div>
                                            <asp:TextBox ID="txtValor" ClientIDMode="Static" onKeyPress="return(MascaraMoeda(this,'.',',',event))" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label style="font-size: small">Tipo:</label>
                                        <asp:DropDownList ID="ddlTipoCusto" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCusto_SelectedIndexChanged" />
                                    </div>
                                    <div class="form-group">
                                        <label style="font-size: small">Tipo de Custo:</label>
                                        <asp:TextBox ID="txtDescricaoCusto" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Data:</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDataCusto" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <asp:LinkButton runat="server" ID="btnCalendario"><span class=" glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                <ajaxToolkit:CalendarExtender CssClass="calendar" TargetControlID="txtDataCusto" ID="calendarExtender" PopupButtonID="btnCalendario" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label style="font-size: small">Observação:</label>
                                        <asp:TextBox ID="txtObsCusto" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton ID="btnSalvarCusto" OnClick="btnSalvarCusto_Click" OnClientClick="return confirm('Deseja Adicionar esse custo?');" CssClass="btn btn-primary" runat="server">Salvar</asp:LinkButton>
                            </div>
                        </div>
                    </div>
            </asp:Panel>
            <!-- FIM MODAL ADICIONAR DESPESAS-->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSalvarCusto" />
            <asp:AsyncPostBackTrigger ControlID="ddlTipoCusto" />
            <asp:AsyncPostBackTrigger ControlID="btnCalcelModalCusto" />
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

