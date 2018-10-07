<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="Presentacion.dashboard" %>

<asp:Content ID="contentTitle" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="contentHeader" ContentPlaceHolderID="Header" runat="server">
	<section class="container" style="background-image: url(img/banner_principal.jpg)">
		<div class="align-content-center" style="padding: 20px;">
			<div class="form-inline">
				<img src="img/ecys.png" style="width: 150px; height: auto; background-color: white; border-radius: 50%" alt="" />
				&nbsp; &nbsp; &nbsp; &nbsp;
				<h1 class="text-capitalize pull-right text-white">Plataforma de gestión administrativa</h1>
			</div>
		</div>
		<hr />
	</section>

</asp:Content>
<asp:Content ID="contentContent" ContentPlaceHolderID="Contenido" runat="server">
	<section class="container-fluid">
		<div class="row">
			<div class="col-lg-6 col-12">
				<div class="card">
					<div class="card-header">
						<h3>iniciar sesión</h3>
					</div>
					<div class="card-body">
						<div class="form-horizontal  d-block">
							<div class="form-group">
								<asp:RequiredFieldValidator ValidationGroup="Sesion" ControlToValidate="txtUsuario" SetFocusOnError="true" ToolTip="Campo obligatorio" runat="server" ID="valUsuario" Display="Dynamic" Style="font-size: 10px;" />
								<asp:TextBox CssClass="form-control" placeholder="Usuario" runat="server" ID="txtUsuario" CausesValidation="true" />
							</div>
							<div class="form-group">
								<asp:RequiredFieldValidator ValidationGroup="Sesion" ControlToValidate="txtContrasena" SetFocusOnError="true" ToolTip="Campo obligatorio" runat="server" ID="valContrasena" Display="Dynamic" Style="font-size: 10px;" />
								<asp:TextBox CssClass="form-control" TextMode="Password" placeholder="Contraseña" runat="server" ID="txtContrasena" CausesValidation="true" />
							</div>
							<asp:PlaceHolder runat="server" ID="AlertaResultado" Visible="true" />
							<asp:Button ValidationGroup="Sesion" OnClick="IniciarSesion" runat="server" ID="BtnEntrar" Text="Entrar" CssClass="btn btn-block btn-outline-warning" CausesValidation="true" />
							<a href="#recPass" class="badge badge-pill badge-primary" data-toggle="modal">¿Olvídaste tu contraseña? </a>
							<span class="pull-right">¿No tienes cuenta? <a href="#Registrarse" class="badge badge-success" data-toggle="collapse">Regístrate</a></span>
						</div>
					</div>
				</div>
			</div>
			<div class="col-lg-6 col-12">
				<div id="Registrarse" class="collapse">
					<div class="card">
						<div class="card-body">
							<h4 class="text-center">¿Eres un estudiante? Completa este formulario y regístrate.</h4>
							<asp:ValidationSummary runat="server" CssClass="badge badge-info" ValidationGroup="Registro" HeaderText="Hay errores. Pasa el mouse encima del(los) icono(s) para más información" />
							<asp:RequiredFieldValidator ToolTip="Carnet obligatorio" ControlToValidate="TxtCarnet" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-warning fa fa-hashtag" style="font-size: 10px;" runat="server" ></asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator ToolTip="Nombre obligatorio" ControlToValidate="TxtNombre" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-warning fa fa-address-book" style="font-size: 10px;" runat="server" ></asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator ToolTip="Apellido obligatorio" ControlToValidate="TxtApellido" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-warning fa fa-address-book-o" style="font-size: 10px;" runat="server" ></asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator ToolTip="Usuario obligatorio" ControlToValidate="TxtUserName" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-warning fa fa-user" style="font-size: 10px;" runat="server" ></asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator ToolTip="Correo electrónico obligatorio" ControlToValidate="TxtEmail" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-warning fa fa-at" style="font-size: 10px;" runat="server" ></asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator ToolTip="Teléfono obligatorio" ControlToValidate="TxtTelefono" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-warning fa fa-phone" style="font-size: 10px;" runat="server" ></asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator ToolTip="Fecha de nacimiento obligatoria" ControlToValidate="TxtFecha" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-warning fa fa-calendar-o" style="font-size: 10px;" runat="server" ></asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator ToolTip="Contraseña obligatoria" ControlToValidate="TxtPassword" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-warning fa fa-lock" style="font-size: 10px;" runat="server" ></asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator ToolTip="Clave obligatoria" ControlToValidate="TxtClave" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-warning fa fa-unlock-alt" style="font-size: 10px;" runat="server" ></asp:RequiredFieldValidator>
							<asp:CompareValidator ToolTip="Contraseña no coincide" ControlToValidate="TxtPassword" ControlToCompare="TxtPasswordRe" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-danger fa fa-lock" style="font-size: 10px;" runat="server"  />
							<asp:CompareValidator ToolTip="Clave no coincide" ControlToValidate="TxtClave" ControlToCompare="TxtClaveRe" ValidationGroup="Registro" Display="Dynamic" SetFocusOnError="true" CssClass="badge badge-danger fa fa-unlock-alt" style="font-size: 10px;" runat="server"  />
							<asp:RegularExpressionValidator ToolTip="Correo electrónico inválido" ControlToValidate="TxtEmail" SetFocusOnError="true" ValidationGroup="Registro" Display="Dynamic" style="font-size: 10px;" CssClass="badge badge-danger fa fa-at" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />
							<asp:RegularExpressionValidator ToolTip="Número de carnet inválido" ControlToValidate="TxtCarnet" SetFocusOnError="true" ValidationGroup="Registro" Display="Dynamic" style="font-size:10px;" CssClass="badge badge-danger fa fa-hashtag" ValidationExpression="\d{9}" runat="server" />
							<asp:RegularExpressionValidator ToolTip="Número de teléfono inválido" ControlToValidate="TxtTelefono" SetFocusOnError="true" ValidationGroup="Registro" Display="Dynamic" style="font-size:10px;" CssClass="badge badge-danger fa fa-phone" ValidationExpression="\d{8}" runat="server" />
							<asp:PlaceHolder ID="PHRegistro" runat="server" />
							<hr />
							<div class="form-horizontal">
								<div class="row form-group">
									<div class="col-lg-6 col-12">
										<asp:Label CssClass="single-input" Text="Carnet de estudiante" runat="server" ID="Label"></asp:Label>
									</div>
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" placeholder="No. de Carnet" runat="server" CausesValidation="true" TextMode="Number"  ID="TxtCarnet"></asp:TextBox>
									</div>
								</div>
								<div class="row form-group">
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" placeholder="Nombres" CausesValidation="true" runat="server" ID="TxtNombre"></asp:TextBox>
									</div>
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" placeholder="Apellidos" CausesValidation="true" runat="server" ID="TxtApellido"></asp:TextBox>
									</div>
								</div>

								<div class="row form-group">
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" placeholder="Usuario" CausesValidation="true" runat="server" ID="TxtUserName"></asp:TextBox>
									</div>
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" placeholder="Correo electrónico" CausesValidation="true" runat="server" ID="TxtEmail"></asp:TextBox>
									</div>
								</div>
								<div class="row form-group">
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" placeholder="Celular" TextMode="Number" CausesValidation="true" runat="server" ID="TxtTelefono"></asp:TextBox>
									</div>
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" ToolTip="Fecha de nacimiento" CausesValidation="true" placeholder="Fecha de nacimiento" TextMode="Date" runat="server" ID="TxtFecha"></asp:TextBox>
									</div>
								</div>
								<div class="row form-group">
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" TextMode="Password" CausesValidation="true" placeholder="Contraseña" runat="server" ID="TxtPassword"></asp:TextBox>
									</div>
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" TextMode="Password" CausesValidation="true" placeholder="Repite contraseña" runat="server" ID="TxtPasswordRe"></asp:TextBox>
									</div>
								</div>
								<div class="row form-group">
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" placeholder="Palabra clave" CausesValidation="true" runat="server" ID="TxtClave"></asp:TextBox>
									</div>
									<div class="col-lg-6 col-12">
										<asp:TextBox CssClass="form-control" TextMode="Password" CausesValidation="true" placeholder="Repite palabra clave" runat="server" ID="TxtClaveRe"></asp:TextBox>
									</div>
								</div>
							</div>
						</div>
						<div class="card-footer">
							<asp:Button runat="server" ID="BtnRegistrar" Text="Registrarse" CausesValidation="true" ValidationGroup="Registro" CssClass="btn btn-success btn-block" />
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
</asp:Content>
<asp:Content ID="contentFooter" ContentPlaceHolderID="Footer" runat="server" >

</asp:Content>
<asp:Content ID="contentAfterFooter" ContentPlaceHolderID="AfterFooter" runat="server">
	<!-- Modal -->
	<div id="recPass" class="modal fade" role="dialog">
		<div class="modal-dialog">
			<!-- Modal content-->
			<div class="modal-content">
				<div class="modal-header">
					<h4 class="h4 modal-title" style="color: black;">Recuperar contraseña</h4>
					<button type="button" class="close" data-dismiss="modal">&times;</button>
				</div>
				<div class="modal-body form-horizontal">
					<p class=" text-justify" style="color: black;">Para recuperar tu contraseña específica la palabra clave y el correo electrónico que utilizaste al registarte.</p>
					<div class="form-group">
						<asp:TextBox TextMode="Password" Style="color: black;" placeholder="Palabra clave" runat="server" ValidationGroup="Recuperar" CausesValidation="true" CssClass="form-control" ID="TxtPalabraClave" />
						<br />
						<asp:TextBox Style="color: black;" placeholder="Correo electrónico" runat="server" ValidationGroup="Recuperar" CausesValidation="true" CssClass="form-control" ID="TxtCorreo" />
					</div>
					<asp:RequiredFieldValidator ValidationGroup="Recuperar" runat="server" ID="ValClave" Display="None" SetFocusOnError="true" ControlToValidate="TxtPalabraClave" />
					<asp:RequiredFieldValidator ValidationGroup="Recuperar" runat="server" ID="ValCorreo" Display="None" SetFocusOnError="true" ControlToValidate="TxtCorreo" />
				</div>
				<div class="modal-footer">
					<asp:Button runat="server" OnClick="RecuperarContrasena" ID="BtnMostrar" CssClass="btn btn-outline-primary"  CausesValidation="true" ValidationGroup="Recuperar" Text="Aceptar" />
				</div>
			</div>

		</div>
	</div>
</asp:Content>

