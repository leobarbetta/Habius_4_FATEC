<%@ Page Title="Editar Meus Dados" Language="C#" MasterPageFile="~/Pages/Administrativo/Cliente/MasterCliente.master" AutoEventWireup="true" CodeFile="EditarMeusDados.aspx.cs" Inherits="Pages_Administrativo_Cliente_EditarMeusDados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../../JavaScript/Mask.js"></script>
   
    <asp:UpdatePanel runat="server" UpdateMode="Always" ID="update1">
        <ContentTemplate>
            <div id="divMensagem" runat="server">
                <asp:Label ID="lblMensagem" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="main" class="container-fluid formCadastro">
        <asp:Panel runat="server">
            <h1 class="page-header text-center" style="color: rgba(77, 77, 77, 0.90)">Meus dados</h1>


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
                                        <label>Nome Completo:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtNome" class="form-control input-lg" runat="server" minlength="10" required oninvalid="setCustomValidity('Por favor, preencha seu nome.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Email:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control input-lg" type="email" required pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$" oninvalid="setCustomValidity('Por favor, insira um email válido.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>RG:</label><asp:Label runat="server" ID="AsteriscoRG" Style="padding-left: 5px" Text="*" ForeColor="Red" />
                                        <asp:TextBox ID="txtRg" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente números" MaxLength="12" pattern="\d{2}\.\d{3}\.\d{3}-\d{1}" required minlength="12" oninvalid="setCustomValidity('Por favor, insira um RG válido.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div id="teste" runat="server" class="form-group">
                                        <label class="control-label" for="txtCPF">CPF:</label><asp:Label runat="server" ID="AsteriscoCPF" Style="padding-left: 5px" Text="*" ForeColor="Red" />
                                        <asp:TextBox ID="txtCPF" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente números" MaxLength="14" pattern="\d{3}\.\d{3}\.\d{3}-\d{2}" required oninvalid="setCustomValidity('Por favor, insira um CPF válido.')" onchange="try{setCustomValidity('')}catch(e){}"/>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Data Nascimento:</label><asp:Label runat="server" ID="AsteriscoData" Style="padding-left: 5px" Text="*" ForeColor="Red" />
                                        <asp:TextBox ID="txtDataNascimento" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente números" MaxLength="10" pattern="[0-9]{2}\/[0-9]{2}\/[0-9]{4}$" required oninvalid="setCustomValidity('Por favor, preencha sua data de nascimento.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sexo:</label><asp:Label runat="server" ID="AsteriscoSexo" required Style="padding-left: 5px" Text="*" ForeColor="Red" />
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
                                        <label>CNPJ:</label><asp:Label runat="server" ID="AsteriscoCnpj" Style="padding-left: 5px" Text="" ForeColor="Red" />
                                        <asp:TextBox ID="txtCnpj" runat="server" ClientIDMode="Static" class="form-control input-lg" placeholder="Somente números" MaxLength="18" Enabled="false"  pattern="\d{2}.?\d{3}.?\d{3}/?\d{4}-?\d{2}" required oninvalid="setCustomValidity('Por favor, insira um CNPJ válido.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Estado Civil:</label><asp:Label runat="server" ID="AsteriscoEstadoCivil" Style="padding-left: 5px" Text="*" ForeColor="Red" required />
                                        <asp:DropDownList ID="ddlEstadoCivil" class="form-control input-lg" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Telefone (+DDD):<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtTelefone" runat="server" ClientIDMode="Static" class="form-control input-lg" placeholder="Somente números" MaxLength="12" OnKeyPress="Mask('## ####-####', this)" required pattern="\([0-9]{2}\) [0-9]{4,6}-[0-9]{3,4}$" oninvalid="setCustomValidity('Por favor, insira seu telefone.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Celular (+DDD):</label>
                                        <asp:TextBox ID="txtCelular" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente números" MaxLength="13" OnKeyPress="Mask('## #####-####', this)" />
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
                                        <label for="txtEndereco">Endereço:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtEndereco" runat="server" class="form-control input-lg" required minlength="10" oninvalid="setCustomValidity('Por favor, preencha seu endereço.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Bairro:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtBairro" class="form-control input-lg" runat="server" required minlength="4" oninvalid="setCustomValidity('Por favor, preencha seu bairro.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Numero:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtNumero" runat="server" class="form-control input-lg"  required oninvalid="setCustomValidity('Por favor, insira o numero de sua residência.')" onchange="try{setCustomValidity('')}catch(e){}" />
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
                                        <label>CEP:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtCEP" ClientIDMode="Static" runat="server" class="form-control input-lg" placeholder="Somente números" MaxLength="9" OnKeyPress="Mask('#####-###', this)" pattern="(^\d{5}-\d{3}|^\d{2}.\d{3}-\d{3}|\d{8})" required oninvalid="setCustomValidity('Por favor, insira seu CEP.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Estado:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:DropDownList ID="ddlEstado" class="form-control input-lg" runat="server" AutoPostBack="true" required OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="update" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cidade:<span style="color: red; padding-left: 5px">*</span></label>
                                                <asp:DropDownList ID="ddlCidade" class="form-control input-lg" ClientIDMode="Static" required runat="server" />
                                            </div>
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
                                        <label>Usuário:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtLogin" class="form-control input-lg" runat="server" required oninvalid="setCustomValidity('Por favor, insira seu nome de usuário.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2 col-sm-offset-4">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-lg active btn-block btn-success" CausesValidation="true" Text="Salvar" OnClick="btnSalvar_Click" />
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="btnVoltar" runat="server" CausesValidation="false" CssClass="btn btn-block active btn-lg btn-danger" Text="Voltar" OnClientClick="javascript: window.history.back(-1);" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

