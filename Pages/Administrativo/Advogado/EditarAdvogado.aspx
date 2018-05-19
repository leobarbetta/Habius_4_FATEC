<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" CodeFile="EditarAdvogado.aspx.cs" Inherits="Pages_Administrativo_Advogado_EditarAdvogado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../../JavaScript/Mask.js"></script>
    <asp:UpdatePanel ID="updateTopo" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="divMensagem" runat="server">
                <asp:Label ID="lblMensagem" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" DefaultButton="btnSalvar">


        <h1 class="text-center page-header text" style="color: rgba(77, 77, 77, 0.90)">EDITAR PERFIL</h1>

        <div class="formCadastro">

            <div class="row">
                <div class="col-md-10 col-md-offset-1">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span class="glyphicon glyphicon-user" style="padding-right: 5px" aria-hidden="true"></span>Dados Pessoais</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label" for="txtNome">Nome Completo:</label>
                                        <asp:TextBox ID="txtNome" class="form-control input-lg" runat="server" minlength="10" required oninvalid="setCustomValidity('Por favor, preencha seu nome.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label" for="txtEmail">Email:</label>
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control input-lg" type="email" required pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$" oninvalid="setCustomValidity('Por favor, insira um email válido.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>RG:</label>
                                        <asp:TextBox ID="txtRg" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente Numeros" MaxLength="12" required minlength="12" pattern="\d{2}\.\d{3}\.\d{3}-\d{1}" oninvalid="setCustomValidity('Por favor, insira um RG válido.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtCPF">CPF:</label>
                                        <asp:TextBox ID="txtCPF" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente Numeros" MaxLength="14" required pattern="\d{3}\.\d{3}\.\d{3}-\d{2}" oninvalid="setCustomValidity('Por favor, insira um CPF válido.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Data de Nascimento:</label>
                                        <asp:TextBox ID="txtDataNascimento" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente Numeros" MaxLength="10" pattern="[0-9]{2}\/[0-9]{2}\/[0-9]{4}$" required oninvalid="setCustomValidity('Por favor, preencha sua data de nascimento.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sexo:</label>
                                        <asp:DropDownList ID="ddlSexo" class="form-control input-lg" runat="server">
                                            <asp:ListItem Value="0" Text="Selecione" />
                                            <asp:ListItem Value="M" Text="Masculino" />
                                            <asp:ListItem Value="F" Text="Feminino" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>OAB:</label>
                                        <asp:TextBox ID="txtOab" runat="server" class="form-control input-lg" MaxLength="10" required oninvalid="setCustomValidity('Por favor, insira seu nº OAB.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Estado Civil:</label>
                                        <asp:DropDownList ID="ddlEstadoCivil" class="form-control input-lg" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Telefone (+DDD):</label>
                                        <asp:TextBox ID="txtTelefone" runat="server" class="form-control input-lg" placeholder="Somente Numeros" MaxLength="12" OnKeyPress="Mask('## ####-####', this)" required pattern="\([0-9]{2}\) [0-9]{4,6}-[0-9]{3,4}$" oninvalid="setCustomValidity('Por favor, insira seu telefone.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Celular (+DDD):</label>
                                        <asp:TextBox ID="txtCelular" class="form-control input-lg" runat="server" placeholder="Somente Numeros" MaxLength="13" OnKeyPress="Mask('## #####-####', this)" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-md-10 col-md-offset-1">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span class="glyphicon glyphicon-menu-hamburger" style="padding-right: 5px" aria-hidden="true"></span>Dados de Endereço</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Endereço:</label>
                                        <asp:TextBox ID="txtEndereco" runat="server" class="form-control input-lg" required minlength="10" oninvalid="setCustomValidity('Por favor, preencha seu endereço.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Bairro:</label>
                                        <asp:TextBox ID="txtBairro" class="form-control input-lg" runat="server" required minlength="4" oninvalid="setCustomValidity('Por favor, preencha seu bairro.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Numero:</label>
                                        <asp:TextBox ID="txtNumero" runat="server" class="form-control input-lg" required oninvalid="setCustomValidity('Por favor, insira o numero de sua residência.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Complemento:</label>
                                        <asp:TextBox ID="txtComplemento" runat="server" class="form-control input-lg" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>CEP:</label>
                                        <asp:TextBox ID="txtCEP" runat="server" class="form-control input-lg" MaxLength="9" OnKeyPress="Mask('#####-###', this)" required pattern="(^\d{5}-\d{3}|^\d{2}.\d{3}-\d{3}|\d{8})" oninvalid="setCustomValidity('Por favor, insira seu CEP.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Estado:</label>
                                        <asp:DropDownList ID="ddlEstado" class="form-control input-lg" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <asp:UpdatePanel ID="updateCidade" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <label>Cidade:</label>
                                                <asp:DropDownList ID="ddlCidade" class="form-control input-lg" runat="server" />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlEstado" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-10 col-md-offset-1">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span class="glyphicon glyphicon-lock" style="padding-right: 5px" aria-hidden="true"></span>Dados de Login</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Usuário:</label>
                                        <asp:TextBox ID="txtLogin" class="form-control input-lg" runat="server" required oninvalid="setCustomValidity('Por favor, insira seu nome de usuário.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>


        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <asp:LinkButton ID="btnSalvar" runat="server" CssClass="btn btn-block btn-primary btn-lg" OnClick="btnSalvar_Click" Text="Atualizar"></asp:LinkButton>
            </div>
        </div>






    </asp:Panel>
</asp:Content>

