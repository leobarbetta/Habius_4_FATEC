<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" CodeFile="CadastraAgenda.aspx.cs" Inherits="Pages_Administrativo_Advogado_CadastraAgenda" %>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../css/tables.css" rel="stylesheet" />
    <link href="../../../HomePage/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../../../JavaScript/Mask.js"></script>

    <h1 class="text-center page-header text" style="color: rgba(77, 77, 77, 0.90)">AGENDA</h1>

    <div class="row">

        <div class="col-md-3 col-md-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading"><span class="glyphicon glyphicon-plus" style="padding-right: 5px" aria-hidden="true"></span>Adicionar Evento</div>
                <div class="panel-body">
                    <asp:UpdatePanel runat="server" ID="update3" UpdateMode="Always">
                        <ContentTemplate>
                            <div id="divMensagem" runat="server">
                                <asp:Label ID="lblMensagem" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Panel runat="server" DefaultButton="btnCadastrar">
                        <div class="form-group">
                            <label>Título</label>
                            <asp:TextBox ID="txtTitulo" class="form-control" runat="server" />
                        </div>
                        <div class="form-group">
                            <label>Descrição</label>
                            <asp:TextBox ID="txtDescricao" class="form-control" runat="server" Rows="4" TextMode="MultiLine" Height="90px" />
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <label>Data:</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtData" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <asp:LinkButton runat="server" ID="btnCalendario"><span class=" glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                    <ajaxToolkit:CalendarExtender CssClass="calendar" TargetControlID="txtData" ID="calendarExtender2" PopupPosition="BottomRight" PopupButtonID="btnCalendario" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:AsyncPostBackTrigger ControlID="calendar" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Horário</label>
                                    <asp:TextBox ID="txtHora" class="form-control" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                        </div>
                        <asp:LinkButton ID="btnCadastrar" runat="server" CssClass="btn btn-block btn-primary btn-lg" Text="Agendar" OnClick="btnCadastrar_Click" />
                        <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-block btn-primary btn-lg" Text="Atualizar" OnClick="btnUpdate_Click" Visible="false" />
                        <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-block btn-danger btn-lg" Text="Cancelar" OnClick="btnCancelar_Click" Visible="false" />
                    </asp:Panel>
                </div>
            </div>

        </div>

        <div class="col-md-7 ">
            <div class="panel panel-default">
                <div class="panel-heading"><span class="glyphicon glyphicon-calendar" style="padding-right: 5px" aria-hidden="true"></span>Eventos</div>
                <div class="panel-body" style="max-height: 630px">
                    <div class="content table-responsive">
                        <asp:UpdatePanel ID="updateGridAgenda" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="gdvAgenda" PageSize="6" OnSorting="gdvAgenda_Sorting" OnPageIndexChanging="gdvAgenda_PageIndexChanging" AllowPaging="true" OnRowCommand="gdvAgenda_RowCommand" runat="server" OnRowDataBound="gdvAgenda_RowDataBound" RowStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" CellPadding="4" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="age_titulo" SortExpression="age_titulo" HeaderText="Título" />
                                        <asp:BoundField DataField="age_descricao" HeaderText="Descrição" SortExpression="age_descricao" />
                                        <asp:BoundField DataField="age_datafinalizacao" DataFormatString="{0:d}" HeaderText="Data" SortExpression="age_datafinalizacao" />
                                        <asp:BoundField DataField="age_datafinalizacao" DataFormatString="{0:t}" HeaderText="Hora" SortExpression="age_datafinalizacao" />
                                        <asp:TemplateField HeaderText="Ações">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" title="Editar" CommandName="Editar" CommandArgument='<% #Bind("AGE_CODIGO")%>'><span style="color:#1b3ec6" class="glyphicon glyphicon-pencil" aria-hidden="true"></span></asp:LinkButton>
                                                 <asp:LinkButton ID="btnFinalizar" runat="server" title="Finalizar" CommandName="Finalizar" CommandArgument='<% #Bind("AGE_CODIGO")%>'><span style="color:#1cb549" class="mif-checkmark" aria-hidden="true"></span></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle HorizontalAlign="Center"></EditRowStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center"></RowStyle>
                                    <PagerSettings PageButtonCount="5" NextPageText="Proxima" PreviousPageText="Anterior" />
                                </asp:GridView>
                                <p class="text-primary small">
                                    <asp:Label ID="lblqtd" runat="server" />
                                </p>
                                <ul class="list-inline" style="padding-top:30px; font-size:medium; font-weight:500">
                                    <li><span style="padding-right:5px; color:#ffcbcb" class="fa fa-circle" aria-hidden="true"></span>Eventos Passados</li>
                                    <li><span style="padding-right:5px; color:#eeffed" class="fa fa-circle" aria-hidden="true"></span>Eventos do Dia</li>
                                    <li><span style="padding-right:5px; color:#ffd8c1" class="fa fa-circle" aria-hidden="true"></span>Eventos Futuros</li>
                                </ul>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
