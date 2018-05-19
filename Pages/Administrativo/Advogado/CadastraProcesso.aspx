<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" CodeFile="CadastraProcesso.aspx.cs" Inherits="Pages_Administrativo_Advogado_CadastraProcesso" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../../JavaScript/Mask.js"></script>
    <asp:UpdatePanel runat="server" ID="update" UpdateMode="Always">
        <ContentTemplate>
            <div id="divMensagem" runat="server">
                <asp:Label ID="lblMensagem" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <h1 class="text-center page-header text" style="color: rgba(77, 77, 77, 0.90)">Cadastrar Processo</h1>

    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><span class="glyphicon glyphicon-folder-open" aria-hidden="true" style="padding-right: 10px"></span>Dados do Processo</h3>
                </div>
                <div class="panel-body">
                    <div class="row" style="padding-top: 10px; padding-bottom: 10px">
                        <div class="col-md-6">
                            <div class="form-inline">
                                <div class="form-group">
                                    <label>Cliente:<span style="color: red; padding-left: 5px">*</span></label>
                                    <asp:DropDownList ID="ddlCliente" class="form-control input-lg" Width="400px" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr style="width: 100%" />
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Numero:<span style="color: red; padding-left: 5px">*</span></label>
                                <asp:TextBox ID="txtNumero" class="form-control input-lg" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Assunto:<span style="color: red; padding-left: 5px">*</span></label>
                                <asp:TextBox ID="txtAssunto" class="form-control input-lg" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Classe:<span style="color: red; padding-left: 5px">*</span></label>
                                <asp:DropDownList ID="ddlClasse" class="form-control input-lg" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Movimentação:<span style="color: red; padding-left: 5px">*</span></label>
                                <asp:DropDownList ID="ddlMovimentacao" class="form-control input-lg" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Data de Cadastro:<span style="color: red; padding-left: 5px">*</span></label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDataCadastro" data-role="datepicker" ClientIDMode="Static" class="form-control input-lg" runat="server" />
                                    <div class="input-group-addon">
                                        <asp:LinkButton runat="server" ID="btnCalendario"><span class=" glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                        <ajaxToolkit:CalendarExtender CssClass="calendar" TargetControlID="txtDataCadastro" ID="calendarExtender" PopupButtonID="btnCalendario" PopupPosition="BottomRight" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Estado:<span style="color: red; padding-left: 5px">*</span></label>
                                <asp:DropDownList ID="ddlEstado" runat="server" class="form-control input-lg" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" />
                            </div>
                        </div>
                        <asp:UpdatePanel runat="server" ID="updateCidade" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cidade/Comarca:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:DropDownList ID="ddlCidade" class="form-control input-lg" runat="server" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlEstado" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Posição do Cliente:<span style="color: red; padding-left: 5px">*</span></label>
                                <asp:DropDownList ID="ddlPosicao" class="form-control input-lg" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Recurso:</label>
                                <br />
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updateRecurso">
                                    <ContentTemplate>
                                        <asp:RadioButtonList runat="server" ID="rblRecurso" AutoPostBack="true" class="radio-inline" OnSelectedIndexChanged="rblRecurso_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            <asp:ListItem Value="1" Text="Sim" />
                                        </asp:RadioButtonList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rblRecurso" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel1">
                                    <ContentTemplate>
                                        <label>Tribunal:</label>
                                        <asp:DropDownList ID="ddlTribunal" class="form-control input-lg" runat="server" Enabled="false" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rblRecurso" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <label>Camara:</label>
                                        <asp:TextBox ID="txtCamara" runat="server" class="form-control input-lg" Enabled="false" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rblRecurso" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Vara:<span style="color: red; padding-left: 5px">*</span></label>
                                <asp:DropDownList ID="ddlVara" class="form-control input-lg" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Data da Audiência:</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDataProcesso" ClientIDMode="Static" data-role="datepicker" class="form-control input-lg" runat="server" />
                                    <div class="input-group-addon">
                                        <asp:LinkButton runat="server" id="btnCalendario2"><span class=" glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                        <ajaxToolkit:CalendarExtender TargetControlID="txtDataProcesso" CssClass="calendar" ID="calendarExtender1" PopupPosition="BottomRight" PopupButtonID="btnCalendario2" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Horário da Audiência:</label>
                                <asp:TextBox ID="txtHoraProcesso" ClientIDMode="Static" class="form-control input-lg" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Descrição:<span style="color: red; padding-left: 5px">*</span></label>
                                <asp:TextBox ID="txtDescricao" class="form-control input-lg" runat="server" Rows="4" TextMode="MultiLine" Height="90px" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Observações:</label>
                                <asp:TextBox ID="txtObservacao" runat="server" class="form-control input-lg" Rows="4" TextMode="MultiLine" Height="90px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-2 col-sm-offset-4">
            <asp:LinkButton ID="btnCadastrar" runat="server" CssClass="btn btn-lg btn-block btn-success" CausesValidation="true" Text="Cadastrar" OnClick="btnCadastrar_Click" />
        </div>
        <div class="col-sm-2">
            <asp:LinkButton ID="btnVoltar" runat="server" CausesValidation="false" OnClick="btnVoltar_Click" CssClass="btn btn-block btn-lg btn-danger" Text="Voltar" />
        </div>
    </div>


</asp:Content>

