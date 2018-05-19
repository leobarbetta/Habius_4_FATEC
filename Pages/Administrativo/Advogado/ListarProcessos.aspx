<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" CodeFile="ListarProcessos.aspx.cs" Inherits="Pages_Administrativo_Advogado_ListarProcessos" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../../../css/tables.css" rel="stylesheet" />
    <asp:UpdatePanel ID="updateTopo" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="divMensagemTopo" runat="server">
                <asp:Label ID="lblMensagemTopo" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title text-center" style="font-size: xx-large"><span class="glyphicon glyphicon-folder-open" aria-hidden="true" style="padding-right: 20px"></span>Processos em Aberto</h1>
                </div>
                <div class="row">
                    <div class="container-fluid">
                        <div class="panel-body" style="min-height: 700px">
                            <div class="search-custom col-md-4 col-md-offset-8">
                                <div role="search">
                                    <asp:Panel runat="server" DefaultButton="btnBusca">
                                        <div class="input-group input-group-lg">
                                            <asp:TextBox ID="txtBusca" type="text" CssClass="form-control" runat="server" placeholder="Busca"></asp:TextBox>
                                            <div class="input-group-btn">
                                                <asp:LinkButton ID="btnBusca" CssClass="btn btn-default" type="submit" runat="server" OnClick="btnBusca_Click"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>

                            <asp:UpdatePanel ID="updateBusca" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12 table-responsive">
                                        <asp:GridView ID="gdvProcesso" runat="server" PageSize="10" AutoGenerateColumns="false" OnRowCommand="gdvProcesso_RowCommand" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" CssClass="table table-hover table-condensed" CellPadding="6" GridLines="None" OnRowDataBound="gdvProcesso_RowDataBound" OnRowCancelingEdit="gdvProcesso_RowCancelingEdit" OnRowEditing="gdvProcesso_RowEditing" OnRowUpdating="gdvProcesso_RowUpdating" AllowPaging="true" OnPageIndexChanging="gdvProcesso_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="MOVIMENTACAO" Visible="false" />
                                                <asp:TemplateField Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblProcesso" runat="server" Text='<%# Eval("CODIGO_PROCESSO") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblCodigo_movimentacao" runat="server" Text='<%# Eval("CODIGO_MOVIMENTACAO") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cliente">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCliente" runat="server" Text='<%# Bind("NOME_CLIENTE") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblCliente" runat="server" Text='<%# Bind("NOME_CLIENTE") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Número do Processo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("NUMEROPROCESSO") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("NUMEROPROCESSO") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assunto">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssunto" runat="server" Text='<%# Bind("ASSUNTO") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblAssunto" runat="server" Text='<%# Bind("ASSUNTO") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Classe">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClasse" runat="server" Text='<%# Bind("CLASSE") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblClasse" runat="server" Text='<%# Bind("CLASSE") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Posição Cliente">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPosicao" runat="server" Text='<%# Bind("POSICAO_CLIENTE") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblPosicao" runat="server" Text='<%# Bind("POSICAO_CLIENTE") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Movimentação">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlMovimentacao" runat="server" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMovimentacao" runat="server" Text='<%# Bind("MOVIMENTACAO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Alterar">
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="btnUpdate" runat="server" title="Salvar" CommandName="Update"><span style="color:#1cb549" class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
                                                        <asp:LinkButton ID="btnCalcelar" runat="server" title="Cancelar" CommandName="Cancel"><span style="color:#ff0000" class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnMovimentação" runat="server" CssClass="btn btn-warning btn-sm" Text="Movimentação" CommandName="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ações">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDetalhes" runat="server" title="Detalhes" CommandName="Detalhes" CommandArgument='<% #Bind("CODIGO_PROCESSO")%>'><span style="color:#1b3ec6" class="glyphicon glyphicon-list-alt" aria-hidden="true"></span></asp:LinkButton>
                                                        <asp:LinkButton ID="btnEditar" runat="server" title="Editar" CommandName="Editar" CommandArgument='<% #Bind("CODIGO_PROCESSO")%>'><span style="color:#1cb549" class="glyphicon glyphicon-pencil" aria-hidden="true"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Custos">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnCustos" runat="server" title="Custos" CommandName="Custos" CommandArgument='<% #Bind("CODIGO_PROCESSO")%>'><span style="color:#1cb549" class="glyphicon glyphicon-usd" aria-hidden="true"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle HorizontalAlign="Center"></EditRowStyle>
                                            <RowStyle HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <p class="text-primary text-right">
                                            <asp:Label ID="lblQtd" runat="server" />
                                        </p>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBusca" />
                                    <asp:AsyncPostBackTrigger ControlID="gdvProcesso" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSalvar" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="updateModalEditar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- label para rodar modal editar processo -->
            <asp:Label ID="lblModalEditarProcesso"
                runat="server"></asp:Label>
            <!-- fim -->

            <!-- MODAL EDITAR PROCESSO -->
            <ajaxToolkit:ModalPopupExtender ID="modalEditarProcesso" runat="server" PopupControlID="panelEditarProcesso" RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="modalFade" DropShadow="false" TargetControlID="lblModalEditarProcesso">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelEditarProcesso" runat="server">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:LinkButton ID="btnCancelar" OnClick="btnCancelar_Click" runat="server"><span style="color:#ff0000" class="close glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            <h2 class="modal-title text-center">Editar Processo</h2>
                        </div>
                        <div class="modal-body">
                            <div id="divStatusPro" runat="server">
                                <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                            </div>
                            <div class="row" style="padding-top: 10px; padding-bottom: 10px">
                                <div class="col-md-6">
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label style="font-size: small">Cliente:</label>
                                            <asp:DropDownList ID="ddlCliente" class="form-control" Width="350px" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="width: 100%" />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Numero:</label>
                                        <asp:TextBox ID="txtNumero" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Assunto:</label>
                                        <asp:TextBox ID="txtAssunto" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Classe:</label>
                                        <asp:DropDownList ID="ddlClasse" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Data de Cadastro:</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDataCadastro" class="form-control" runat="server" />
                                            <div class="input-group-addon">
                                                <asp:LinkButton runat="server" ID="btnCalendario"><span class=" glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                <ajaxToolkit:CalendarExtender TargetControlID="txtDataCadastro" ID="calendarExtender" PopupButtonID="btnCalendario" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Estado:</label>
                                        <asp:DropDownList ID="ddlEstado" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Cidade/Comarca:</label>
                                        <asp:DropDownList ID="ddlCidade" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Posição do Cliente:</label>
                                        <asp:DropDownList ID="ddlPosicao" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Recurso:</label>
                                        <br />
                                        <asp:RadioButtonList runat="server" Font-Size="Small" ID="rblRecurso" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblRecurso_SelectedIndexChanged" CellPadding="5" CellSpacing="5" RepeatLayout="Flow" TextAlign="Right">
                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            <asp:ListItem Value="1" Text="Sim" />
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Tribunal:</label>
                                        <asp:DropDownList ID="ddlTribunal" class="form-control" runat="server" Enabled="false" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Camara:</label>
                                        <asp:TextBox ID="txtCamara" runat="server" class="form-control" Enabled="false" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Vara:</label>
                                        <asp:DropDownList ID="ddlVara" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Data da Audiência:</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDataProcesso" class="form-control" runat="server" />
                                            <div class="input-group-addon">
                                                <asp:LinkButton runat="server" ID="btnCalendario2"><span class=" glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                <ajaxToolkit:CalendarExtender CssClass="calendar" TargetControlID="txtDataProcesso" ID="calendarExtender1" PopupButtonID="btnCalendario2" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Horario da Audiência:</label>
                                        <asp:TextBox ID="txtHoraAudiencia" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Descrição:</label>
                                        <asp:TextBox ID="txtDescricao" class="form-control" runat="server" Rows="4" TextMode="MultiLine" Height="90px" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Observações:</label>
                                        <asp:TextBox ID="txtObservacao" runat="server" class="form-control" Rows="4" TextMode="MultiLine" Height="90px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnSalvar" OnClick="btnSalvar_Click" CssClass="btn btn-primary" runat="server">Atualizar</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <!-- FIM MODAL EDITAR PROCESSO-->

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
            <asp:AsyncPostBackTrigger ControlID="gdvProcesso" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="updateModalDetalhes" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- label para rodar modal DETALHES processo -->
            <asp:Label ID="lblModalDetalhes"
                runat="server"></asp:Label>
            <!-- fim -->

            <!-- MODAL DETALHES PROCESSO -->
            <ajaxToolkit:ModalPopupExtender ID="modalDetalhes" runat="server" PopupControlID="panelDetalhes" RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="modalFade" DropShadow="false" TargetControlID="lblModalDetalhes">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelDetalhes" runat="server">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:LinkButton ID="btnCancelarDetalhes" OnClick="btnCancelarDetalhes_Click" runat="server"><span style="color:#ff0000" class="close glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            <h2 class="modal-title text-center">Detalhes do Processo</h2>
                        </div>
                        <div class="modal-body">
                            <div id="divEditarPro" runat="server">
                                <asp:Label ID="lblMensagemStatus" runat="server"></asp:Label>
                            </div>
                            <div class="row" style="padding-top: 5px; padding-bottom: 5px">
                                <div class="col-md-12">
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <p class="form-control-static">
                                                <label class="control-label">Cliente: </label>
                                                <asp:Label ID="lblClienteDetalhes" runat="server" />
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="width: 100%" />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label class="control-label">Numero:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblNumeroDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Classe:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblClasseDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Estado:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblEstado" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Posição do Cliente:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblPosicaoDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Tribunal:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblTribunalDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Vara:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblVaraDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Data da Audiência:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblDataAudiencia" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Descrição:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblDescricaoDetalhes" runat="server" />
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label class="control-label">Assunto:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblAssuntoDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Data de Cadastro:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblDataCadastroDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Cidade/Comarca:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblComarcaDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Recurso:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblRecursoDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Camara:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblCamaraDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Movimentação:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblMovDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Horario da Audiência:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblHoraAudienciaDetalhes" runat="server" />
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Observações:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblObservacaoDetalhes" runat="server" />
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <!-- FIM MODAL DETALHES PROCESSO-->


        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCancelarDetalhes" />
            <asp:AsyncPostBackTrigger ControlID="gdvProcesso" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

