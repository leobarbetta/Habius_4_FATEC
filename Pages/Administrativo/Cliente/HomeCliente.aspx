<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Cliente/MasterCliente.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="HomeCliente.aspx.cs" Inherits="Pages_Administrativo_Cliente_HomeCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../css/tables.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-6">
        <div class="panel danger" data-role="panel">
            <div class="heading">
                <span class="title">Meus Processos</span>
            </div>
            <div class="content table-responsive">
                <asp:UpdatePanel ID="updateProcesso" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gdvProcesso" PageSize="5" OnRowCommand="gdvProcesso_RowCommand" OnPageIndexChanging="gdvProcesso_PageIndexChanging" AllowPaging="true" CssClass="table table-hover table-condensed" HeaderStyle-BackColor="White" BackColor="LightSeaGreen" runat="server" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" CellPadding="6" GridLines="None" OnRowDataBound="gdvProcesso_RowDataBound" AutoGenerateColumns="false" Height="10px">
                            <Columns>
                                <asp:BoundField DataField="NUMEROPROCESSO" SortExpression="NUMEROPROCESSO" HeaderText="Processo Nº" />
                                <asp:BoundField DataField="DATA_MOVIMENTACAO" SortExpression="DATA_MOVIMENTACAO" HeaderText="Data Audiencia" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="MOVIMENTACAO" SortExpression="MOVIMENTACAO" HeaderText="Status" />
                                <asp:TemplateField HeaderText="Ações">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDetalhes" runat="server" title="Detalhes" CommandName="Detalhes" CommandArgument='<% #Bind("CODIGO_PROCESSO")%>'><span style="color:#1b3ec6" class="glyphicon glyphicon-list-alt" aria-hidden="true"></span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle HorizontalAlign="Center"></EditRowStyle>
                            <RowStyle HorizontalAlign="Center"></RowStyle>
                            <HeaderStyle ForeColor="White" />
                        </asp:GridView>
                        <p class="text-primary small">
                            <asp:Label ID="lbl" runat="server" />
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

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
                                            <label class="control-label">Advogado: </label>
                                            <p class="form-control-static">
                                            </p>
                                            <asp:Label ID="lblAdvogadoCliente" runat="server" />
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

