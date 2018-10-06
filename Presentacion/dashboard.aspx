<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="Presentacion.dashboard" %>

<asp:Content ID="contentTitle" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="contentHeader" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="contentMenu" ContentPlaceHolderID="Menu" runat="server">
</asp:Content>
<asp:Content ID="contentHome" ContentPlaceHolderID="Home" runat="server">
</asp:Content>
<asp:Content ID="contentContent" ContentPlaceHolderID="Contenido" runat="server">

	<div class="register">
		<div class="container-fluid" style="padding: 4px 0 4px 0;">
			<div class="row row-eq-height">

				<div class="col-lg-6 nopadding">
					<div style="background-image: url(images/fiusac03.jpg)" class="search_section d-flex flex-column align-items-center justify-content-center">
						<div class="form-horizontal">
							<div class="logo form-inline" style="padding-right: 30px; padding-left: 30px;">
								<img src="images/ecys.png" style="width: 90px; height: auto; background-color: white; border-radius: 50%" alt="">
								&nbsp;&nbsp;&nbsp;&nbsp;
								<span style="color: white;">iniciar sesión</span>
							</div>
							<br />
							<div class="form-group form-inline">
								<asp:RequiredFieldValidator ValidationGroup="Sesion" ControlToValidate="txtUsuario" SetFocusOnError="true" ToolTip="Campo obligatorio" runat="server" ID="valUsuario" Display="Dynamic" Style="font-size: 10px;" />
								<asp:TextBox CssClass=" input_field" placeholder="Usuario" runat="server" ID="txtUsuario" CausesValidation="true" />
							</div>
							<div class="form-group form-inline">
								<asp:RequiredFieldValidator ValidationGroup="Sesion" ControlToValidate="txtContrasena" SetFocusOnError="true" ToolTip="Campo obligatorio" runat="server" ID="valContrasena" Display="Dynamic" Style="font-size: 10px;" />
								<asp:TextBox CssClass="input_field" TextMode="Password" placeholder="Contraseña" runat="server" ID="txtContrasena" CausesValidation="true" />
							</div>
							<asp:PlaceHolder runat="server" ID="AlertaResultado" />
							<asp:Button ValidationGroup="Sesion" runat="server" ID="BtnEntrar" Text="Entrar" CssClass="search_submit_button trans_200" CausesValidation="true" />
							<a href="#recPass" class="badge badge-light badge-pill text-center" data-toggle="modal" >Recuperar contraseña</a>
						</div>
					</div>
				</div>

				<div class="col-lg-6 nopadding">
					<div class="register_section d-flex flex-column align-items-center justify-content-center">
						<div class="register_content text-center">
							<div class="logo">
								<span class="h1 register_title">Plataforma administrativa</span>
							</div>
							<hr />
							<div class="card-body">
								<span class="h4 register_title">Registrarse como estudiante</span>
								<p class="register_text">Si eres un estudiante y aún no tienes cuenta, llena el formulario con tus datos y crea tu cuenta para acceder a la plataforma</p>
							</div>
							<asp:Button runat="server" ID="BtnRegistrar" Text="Registrarse" CssClass="search_submit_button trans_200" Style="background-color: black;" CausesValidation="false" />
						</div>
						<br />
						<br />
						<br />
						<hr />
						<span style="color: black;">
							<!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
							Copyright &copy;
							<script>document.write(new Date().getFullYear());</script>
							All rights reserved | <a href="https://colorlib.com" target="_blank">Colorlib</a>
							<!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
						</span>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>

<asp:Content ID="contentFooter" ContentPlaceHolderID="Footer" runat="server">
	<!-- Modal -->
	<div id="recPass" class="modal fade" role="dialog">
		<div class="modal-dialog">

			<!-- Modal content-->
			<div class="modal-content">
				<div class="modal-header">
					<h4 class="h4 modal-title" style="color:black;">Recuperar contraseña</h4>
					<button type="button" class="close" data-dismiss="modal">&times;</button>
				</div>
				<div class="modal-body form-horizontal">
					<p class=" text-justify" style="color:black;">Para recuperar tu contraseña específica la palabra clave y el correo electrónico que utilizaste al registarte.</p>
					<div class="form-group">
						<asp:TextBox TextMode="Password" style="color:black;" placeholder="Palabra clave" runat="server" ValidationGroup="Recuperar" CausesValidation="true" CssClass="form-control" ID="TxtPalabraClave" />
						<br />
						<asp:TextBox style="color:black;" placeholder="Correo electrónico" runat="server" ValidationGroup="Recuperar" CausesValidation="true" CssClass="form-control" ID="TxtCorreo" />
					</div>
					<asp:RequiredFieldValidator ValidationGroup="Recuperar" runat="server" ID="ValClave" Display="None" SetFocusOnError="true" ControlToValidate="TxtPalabraClave" />
					<asp:RequiredFieldValidator ValidationGroup="Recuperar" runat="server" ID="ValCorreo" Display="None" SetFocusOnError="true" ControlToValidate="TxtCorreo" />
				</div>
				<div class="modal-footer">
					<asp:Button runat="server" ID="BtnMostrar" CssClass="btn" style="background-color: rgb(234,116,20); color: white;" CausesValidation="true" ValidationGroup="Recuperar" Text="Aceptar" />
				</div>
			</div>

		</div>
	</div>
</asp:Content>

