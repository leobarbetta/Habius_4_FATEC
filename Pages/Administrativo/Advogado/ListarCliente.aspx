<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" CodeFile="ListarCliente.aspx.cs" Inherits="Pages_Administrativo_Advogado_ListarCliente" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../../../css/tables.css" rel="stylesheet" />

    <asp:UpdatePanel ID="updateTopo" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div runat="server" id="divMensagem">
                <asp:Label ID="lblMensagemTopo" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title text-center" style="font-size: xx-large"><span class="glyphicon glyphicon-user" aria-hidden="true" style="padding-right: 10px"></span>Clientes Cadastrados</h1>
                </div>
                <div class="row">
                    <div class="container-fluid">
                        <div class="panel-body" style="min-height: 700px">
                            <asp:Panel runat="server" DefaultButton="btnBusca">
                                <div class="search-custom col-md-4 col-md-offset-8">
                                    <div role="search" class="no-print">
                                        <div class="input-group input-group-lg">

                                            <asp:TextBox ID="txtBusca" type="text" CssClass="form-control" runat="server" placeholder="Busca"></asp:TextBox>

                                            <div class="input-group-btn">
                                                <asp:LinkButton ID="btnBusca" CssClass="btn btn-default" type="submit" runat="server" OnClick="btnBusca_Click"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:UpdatePanel ID="updateGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12 table-responsive">
                                        <asp:GridView ID="gdvCliente" runat="server" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" CssClass="table table-hover table-condensed"
                                            AutoGenerateColumns="False" CellPadding="6" GridLines="None" OnRowCommand="gdvClienteFisico_RowCommand" OnPageIndexChanging="gdvCliente_PageIndexChanging"
                                            AllowPaging="true" PageSize="10">
                                            <Columns>
                                                <asp:BoundField DataField="CON_NOME" HeaderText="Nome">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_CELULAR" HeaderText="Celular" />
                                                <asp:BoundField DataField="CON_TELEFONE" HeaderText="Telefone" />
                                                <asp:BoundField DataField="DOCS" HeaderText="CPF/CNPJ" />
                                                <asp:BoundField DataField="ATIVO" HeaderText="ATIVO" Visible="false" />
                                                <asp:BoundField DataField="NIV_DESCRICAO" HeaderText="Tipo" />
                                                <asp:TemplateField HeaderText="Ações">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditar" runat="server" title="Editar" CommandName="Editar" CommandArgument='<% #Bind("PES_CODIGO")%>'><span style="color:#1cb549" class="glyphicon glyphicon-pencil" aria-hidden="true"></span></asp:LinkButton>
                                                        &nbsp;
                                                         <asp:LinkButton ID="btnDetalhes" runat="server" title="Detalhes" CommandName="Detalhes" CommandArgument='<% #Bind("PES_CODIGO")%>'><span style="color:#1b3ec6" class="glyphicon glyphicon-list-alt" aria-hidden="true"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pagamento">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnPagamento" runat="server" title="Pagamento" CommandName="Pagamentos" CommandArgument='<% #Bind("PES_CODIGO")%>'><span style="color:#1cb549" class="glyphicon glyphicon-usd" aria-hidden="true"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle HorizontalAlign="Center"></EditRowStyle>
                                            <RowStyle HorizontalAlign="Center"></RowStyle>
                                            <PagerSettings Position="Bottom" Mode="Numeric" PageButtonCount="10" />
                                        </asp:GridView>
                                        <p class="text-primary text-right">
                                            <asp:Label ID="lblQtd" runat="server" />
                                        </p>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBusca" />
                                    <asp:AsyncPostBackTrigger ControlID="gdvCliente" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSalvarCliF" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSalvarCliJu" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="updateDetalhesCliF" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- label para rodar modal detalhes cliente -->
            <asp:Label ID="lblModalDetalhesCliF"
                runat="server"></asp:Label>
            <!-- fim -->
            <!-- MODAL DETALHES CLIENTE FISICO -->
            <ajaxToolkit:ModalPopupExtender ID="modalDetalheCliF" runat="server" PopupControlID="panelDetalhesCliF" RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="modalFade" DropShadow="false"
                PopupDragHandleControlID="panel3" TargetControlID="lblModalDetalhesCliF">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelDetalhesCliF" runat="server">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:LinkButton ID="btnVoltarCliF" OnClick="btnVoltarCliF_Click" runat="server"><span style="color:#ff0000" class="close glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            <h2 class="modal-title text-center" id="modalDetalhesCLiF">
                                <asp:Label ID="lblNomeCliF" runat="server"></asp:Label></h2>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label class="control-label">CPF:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblCPFCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">RG:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblRGCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Data de Nascimento:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblDataNascimentoCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Sexo:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblSexoCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Estado Civil:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblestadoCivilCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Celular:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblCelularCliF" ClientIDMode="Static" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Telefone:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblTelefoneCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Email:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblEmailCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Login:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblLoginCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label class="control-label">Endereço:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblEnderecoCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Nº:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblNumeroCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Bairro:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblBairroCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">CEP:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblCEPCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Cidade:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblCidadeCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Estado:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblEstadoCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Complemento:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblComplementoCliF" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <!-- FIM MODAL DETALHES CLIENTE -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gdvCliente" />
            <asp:AsyncPostBackTrigger ControlID="btnVoltarCliF" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="updateEditarCliF" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- label para rodar modal editar cliente -->
            <asp:Label ID="lblModalEditarCliF"
                runat="server"></asp:Label>
            <!-- fim -->

            <!-- MODAL EDITAR CLIENTE FISICO -->
            <ajaxToolkit:ModalPopupExtender ID="modalEditarCliF" runat="server" PopupControlID="panelEditarCliF" BackgroundCssClass="modalFade" RepositionMode="RepositionOnWindowResizeAndScroll" DropShadow="false" TargetControlID="lblModalEditarCliF">
            </ajaxToolkit:ModalPopupExtender>

            <%--<ajaxToolkit:MaskedEditExtender ID="maskTelefone" TargetControlID="txtTelefoneCliF" runat="server" Mask="(99) 9999-9999" MaskType="Number" InputDirection="RightToLeft" />
            
            <ajaxToolkit:MaskedEditExtender ID="maskcpf" TargetControlID="txtCPFCliF" runat="server" Mask="999.999.999-99" MaskType="Number" InputDirection="RightToLeft" />
            <ajaxToolkit:MaskedEditExtender ID="maskrg" TargetControlID="txtRgCliF" runat="server" Mask="99.999.999-9" MaskType="Number" InputDirection="RightToLeft" />
            <ajaxToolkit:MaskedEditExtender ID="maskCelular" TargetControlID="txtCelularCliF" runat="server" Mask="(99) 99999-9999" MaskType="Number" InputDirection="RightToLeft" />--%>

            <asp:Panel ID="panelEditarCliF" runat="server">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <asp:Panel runat="server">
                            <div class="modal-header">
                                <asp:LinkButton ID="btnCancelarCliF" ForeColor="White" OnClick="btnVoltarCliF_Click" runat="server"><span style="color:#ff0000" class="close glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                                <h2 class="modal-title text-center">Editar Cliente</h2>
                            </div>
                            <div class="modal-body">
                                <div id="divStatusCliF" runat="server">
                                    <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h5 style="text-align: center; color: #337ab7; font-weight: bold">Dados Pessoais</h5>
                                    </div>
                                    <hr style="width: 100%" />
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label style="font-size: small">Nome Completo:</label>
                                            <asp:TextBox ID="txtNomeCliF" class="form-control" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label style="font-size: small">RG:</label>
                                            <asp:TextBox ID="txtRgCliF" ClientIDMode="Static" class="form-control" runat="server" placeholder="Somente Numeros" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label style="font-size: small">CPF:</label>
                                            <asp:TextBox ID="txtCPFCliF" class="form-control" runat="server" placeholder="Somente Numeros" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label style="font-size: small">Data de Nascimento:</label>
                                            <asp:TextBox ID="txtDataNascimentoCliF" class="form-control" runat="server" ClientIDMode="Static" />
                                            <%--<ajaxToolkit:MaskedEditExtender ID="maskDataNascimentoCliF" ClientIDMode="Static" TargetControlID="txtDataNascimentoCliF" runat="server" Mask="99/99/9999" MaskType="Number" />--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label style="font-size: small">Sexo:</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlSexoCliF" runat="server">
                                                <asp:ListItem Value="0" Text="Selecione" />
                                                <asp:ListItem Value="M" Text="Masculino" />
                                                <asp:ListItem Value="F" Text="Feminino" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label style="font-size: small">Telefone (+DDD):</label>
                                            <asp:TextBox ID="txtTelefoneCliF" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Somente Numeros" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label style="font-size: small">Celular (+DDD):</label>
                                            <asp:TextBox ID="txtCelularCliF" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Somente Numeros" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label style="font-size: small">Estado Civil:</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlEstadoCivil" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h5 style="text-align: center; color: #337ab7; font-weight: bold">Dados de Endereço</h5>
                                    </div>
                                    <hr style="width: 100%" />
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label style="font-size: small">Endereço:</label>
                                            <asp:TextBox ID="txtEnderecoCliF" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label style="font-size: small">Bairro:</label>
                                            <asp:TextBox ID="txtBairroCliF" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label style="font-size: small">Numero:</label>
                                            <asp:TextBox ID="txtNumeroCliF" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label style="font-size: small">Complemento:</label>
                                            <asp:TextBox ID="txtComplementoCliF" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label style="font-size: small">CEP:</label>
                                            <asp:TextBox ID="txtCEPCliF" runat="server" CssClass="form-control" MaxLength="9" OnKeyPress="Mask('#####-###', this)" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label style="font-size: small">Estado:</label>
                                            <asp:DropDownList ID="ddlEstadoCliF" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoCliF_SelectedIndexChanged" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label style="font-size: small">Cidade:</label>
                                            <asp:DropDownList ID="ddlCidadeCliF" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h5 style="text-align: center; color: #337ab7; font-weight: bold">Dados de Login</h5>
                                    </div>
                                    <hr style="width: 100%" />
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label style="font-size: small">Email:</label>
                                            <asp:TextBox ID="txtEmailCliF" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label style="font-size: small">Login:</label>
                                            <asp:TextBox ID="txtLoginCliF" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton ID="btnSalvarCliF" OnClick="btnSalvarCliF_Click" CssClass="btn btn-primary" runat="server">Atualizar</asp:LinkButton>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>
            <!-- FIM MODAL EDITAR CLIENTE -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gdvCliente" />
            <asp:AsyncPostBackTrigger ControlID="btnSalvarCliF" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelarCliF" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="updateDetalhesCliJu" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- label para rodar modal detalhes cliente juridico -->
            <asp:Label ID="lblModalDetalhesCliJu"
                runat="server"></asp:Label>
            <!-- fim -->

            <!-- MODAL DETALHES CLIENTE JURIDICO -->
            <ajaxToolkit:ModalPopupExtender ID="modalDetalhesCliJu" runat="server" PopupControlID="panelDetalhesCliJu" RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="modalFade" DropShadow="false"
                PopupDragHandleControlID="panel3" TargetControlID="lblModalDetalhesCliJu">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelDetalhesCliJu" runat="server">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:LinkButton ID="btnoltarCliJu" OnClick="btnVoltarCliF_Click" runat="server"><span style="color:#ff0000" class="close glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            <h2 class="modal-title text-center">
                                <asp:Label ID="lblNomeCliJu" runat="server"></asp:Label></h2>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label class="control-label">CNPJ:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblCNPJCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Celular:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblCelularCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Telefone:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblTelefoneCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Email:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblEmailCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Login:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblLoginCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label class="control-label">Endereço:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblEnderecoCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Nº:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblNumeroCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Bairro:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblBairroCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">CEP:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblCEPCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Cidade:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblCidadeCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Estado:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblEstadoCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label class="control-label">Complemento:</label>
                                            <p class="form-control-static">
                                                <asp:Label ID="lblComplementoCliJu" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <!-- FIM MODAL DETALHES CLIENTE JURIDICO -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gdvCliente" />
            <asp:AsyncPostBackTrigger ControlID="btnoltarCliJu" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="updateEditarCliJu" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- label para rodar modal editar cliente juridico -->
            <asp:Label ID="lblModalEditarCliJu"
                runat="server"></asp:Label>
            <!-- fim -->
            <!-- MODAL EDITAR CLIENTE JURIDICO -->
            <ajaxToolkit:ModalPopupExtender ID="modalEditarCliJu" runat="server" PopupControlID="panelEditarCliJu" RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="modalFade" DropShadow="false" TargetControlID="lblModalEditarCliJu">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelEditarCliJu" runat="server">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:LinkButton ID="btnCancelarCliJu" OnClick="btnVoltarCliF_Click" runat="server"><span style="color:#ff0000" class="close glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            <h2 class="modal-title text-center">Editar Cliente Júridico</h2>
                        </div>
                        <div class="modal-body">
                            <div id="divStatusCliJu" runat="server">
                                <asp:Label ID="lblMensagemJu" runat="server"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h5 style="text-align: center; color: #337ab7; font-weight: bold">Dados da Empresa</h5>
                                </div>
                                <hr style="width: 100%" />
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Nome Completo:</label>
                                        <asp:TextBox ID="txtNomeCliJu" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">CNPJ:</label>
                                        <asp:TextBox ID="txtCnpjCliJu" class="form-control" runat="server" placeholder="Somente Numeros" ClientIDMode="Static" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Telefone (+DDD):</label>
                                        <asp:TextBox ID="txtTelefoneCliJu" class="form-control" runat="server" placeholder="Somente Numeros" ClientIDMode="Static" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Celular (+DDD):</label>
                                        <asp:TextBox ID="txtCelularCliJu" class="form-control" runat="server" placeholder="Somente Numeros" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h5 style="text-align: center; color: #337ab7; font-weight: bold">Dados de Endereço</h5>
                                </div>
                                <hr style="width: 100%" />
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Endereço:</label>
                                        <asp:TextBox ID="txtEnderecoCliJu" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Bairro:</label>
                                        <asp:TextBox ID="txtBairroCliJu" class="form-control" runat="server" placeholder="Bairro" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label style="font-size: small">Numero:</label>
                                        <asp:TextBox ID="txtNumeroCliJu" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label style="font-size: small">Complemento:</label>
                                        <asp:TextBox ID="txtComplementoCliJu" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label style="font-size: small">CEP:</label>
                                        <asp:TextBox ID="txtCEPCliJu" class="form-control" runat="server" MaxLength="9" OnKeyPress="Mask('#####-###', this)" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Estado:</label>
                                        <asp:DropDownList ID="ddlEstadoCliJu" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoCliF_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: small">Cidade:</label>
                                        <asp:DropDownList ID="ddlCidadeCliJu" class="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h5 style="text-align: center; color: #337ab7; font-weight: bold">Dados de Login</h5>
                                </div>
                                <hr style="width: 100%" />
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Email:</label>
                                        <asp:TextBox ID="txtEmailCliJu" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label style="font-size: small">Login:</label>
                                        <asp:TextBox ID="txtLoginCliJu" class="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnSalvarCliJu" OnClick="btnSalvarCliJu_Click" CssClass="btn btn-primary" runat="server">Atualizar</asp:LinkButton>
                        </div>
            </asp:Panel>
            <!-- FIM MODAL EDITAR CLIENTE JURIDICO-->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gdvCliente" />
            <asp:AsyncPostBackTrigger ControlID="btnSalvarCliJu" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelarCliJu" />
        </Triggers>
    </asp:UpdatePanel>



</asp:Content>

