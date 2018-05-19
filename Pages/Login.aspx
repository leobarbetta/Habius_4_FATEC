<%@ Page Language="C#" AutoEventWireup="true" MetaDescription="Habius Advocacia o seu software jurídico" CodeFile="~/Pages/Login.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/metro.min.css" rel="stylesheet" />
    <link href="../css/metro-icons.min.css" rel="stylesheet" />

    <style>
        #formLogin {
            margin-top: 10%;
        }

        html, body, form {
            background-color: #0072C6;
            height: 100%;
        }

        #banner {
            min-height: 100%;
            height: auto;
            background-color: white;
        }
        #imgBanner{
            width:9000px;
        }
        #contBanner{
            padding:25px;
        }
    
        #logoLogin:hover{
            opacity:0.4;
            
        }
        .panel > .heading {
            
            background-color: #A9A9A9;
        }
            .panel > .heading > .icon {
            
                background-color: #696969;
            }
    </style>


    <!--[if lt IE 9]>
     <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
     <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
   <![endif]-->

</head>
<script src="../JavaScript/jquery-1.11.3.js"></script>
<script src="../JavaScript/bootstrap.min.js"></script>
<script src="../JavaScript/metro.min.js"></script>

<body>
    <form id="form1" runat="server" defaultbutton="btnLogin">


        <div class="col-md-3 col-md-offset-3">

            <a href="../index.html"><img id="logoLogin" style="margin-top: 40%" src="../HomePage/img/habiusLogo.png" class="img-responsive" /></a>
            

            <div id="formLogin" class="panel">
                <div class="heading">
                    <span class="icon mif-lock"></span>
                    <span class="title">Login</span>
                </div>
                <div class="content">
                    <div id="divErro" runat="server">
                        <asp:Label ID="lblMensagem" data-dismiss="modal" aria-hidden="true" runat="server" />
                    </div>
                    <div class="row">
                        <div class="align-center">
                            <div class="input-control modern text iconic">
                                <asp:TextBox ID="txtLogin" type="text" runat="server" />
                                <span class="label">Usuário:</span>
                                <span class="informer">Insira seu nome de usuário</span>
                                <span class="placeholder">Usuário</span>
                                <span class="icon mif-user"></span>
                            </div>
                        </div>

                        <div class=" align-center">
                            <div class="input-control modern password iconic" data-role="input">
                                <asp:TextBox ID="txtSenha" runat="server" type="password" />
                                <span class="label">Senha:</span>
                                <span class="informer">Insira sua senha</span>
                                <span class="placeholder">Senha</span>
                                <span class="icon mif-lock"></span>
                                <button style="background: none" class="button helper-button reveal"><span class="mif-looks"></span></button>
                            </div>
                        </div>
                    </div>
                    <div class="grid" style="padding-top: 30px">
                        <div class=" row cells4">
                            <div class="">
                                <asp:LinkButton ID="btnLogin" runat="server" class="button success btn-block" Text="Logar" OnClick="btnLogin_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="banner" class="col-md-3 pull-right">
            <div class="row">
                <img id="imgBanner" src="../Images/img-banner.JPG" />
            </div>
            <div id="contBanner" class="row">
                <h2>Administre seu tempo<br /><br /><small>Não é de grande utilidade sair fazendo tudo o que tiver pela frente. É importante organizar-se e estabelecer um tempo máximo para cada tarefa e trabalho que você tiver. Se não, você pode passar tempo demais em algum processo e esquecer dos outros.</small></h2>
            </div>
            
        </div>

    </form>
</body>
</html>
