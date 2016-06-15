<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true"
    CodeFile="AsigancionAuditorSeguimiento.aspx.cs" Inherits="AsigancionAuditorSeguimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="Scripts/js_angular/angular.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-animate.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-touch.js" type="text/javascript"></script>
    <script src="Styles/bootstrap-dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="angular/controllers/controller_seguimiento.js" type="text/javascript"></script>
    <script src="Scripts/AngularUI/ui-bootstrap-tpls-1.3.3.js" type="text/javascript"></script>
    <link href="Styles/bootstrap-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modal-backdrop.foo { z-index: 99109099; }
    </style>
    <table id="tblTitulo" cellpadding="0" cellspacing="5" style="width: 80%; padding-top: 10px">
        <tr>
            <td align="center" style="height: 35px; padding-left: 15px; padding-top: 15px; padding-bottom: 15px">
                <asp:Label ID="lblTipoGestion" runat="server" Font-Bold="True" Font-Size="20pt" Font-Underline="false"
                    Font-Italic="True" ForeColor="black" Text="ASIGNACION DE AUDITORES" Font-Names="Arno Pro"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="ng-app" ng-app="myApp" ng-controller="controller_seguimiento">
        <uib-accordion close-others="false">
   

    <uib-accordion-group  panel-class="panel-info">
      <uib-accordion-heading >
        Presentadas EN TERMINO <i class="pull-right glyphicon" ng-class="{'glyphicon-chevron-down': status.open, 'glyphicon-chevron-right': !status.open}"></i>
      </uib-accordion-heading>
      
       <table id="example" class="table table-striped table-bordered datatable table-hover ">
            <thead>
                <tr>
                   
                    <th>
                    </th>
                    <th>
                        Id
                    </th>
                    <th>
                        Patente
                    </th>
                    <th>
                        Modelo
                    </th>
                    <th>
                        <span style="padding-right:10px">Auditor</span>
                        <button type="button" class="btn btn-primary btn-xs" ng-click="open()" >...</button>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr ng-repeat="item in Vehiculos  ">
                <td align="center" style="width: 85px" >
                        <center>
                            <span>
                                <asp:Image ng-click="EditAccess($event,item);" ImageUrl="~/images/edit.gif"
                                    ID="Image4" runat="server" Style="cursor: hand;" /></span>
                        </center>
                    </td>
                    <td align="left" >
                        <span>{{item.Id}}</span>
                    </td>
                    <td align="left" >
                        <span>{{item.Patente}}</span>
                    </td>
                    <td align="left" >
                        <span>{{item.Modelo}}</span>
                    </td>
                    <td>
                    <select id="cboDepartamentos">
                                    <option value="Mendoza" selected="selected">Juan</option>
                                    <option value="General Alvear">Diego</option>
                                    <option value="Godoy Cruz">Gustavo</option>
                                    <option value="Guaymallén">etc</option>
                                    
                                </select>
                    </td>
                    
                </tr>
            </tbody>
        </table>

    </uib-accordion-group>

    <uib-accordion-group  panel-class="panel-info">
      <uib-accordion-heading >
        Presentadas FUERA DE TERMINO <i class="pull-right glyphicon" ng-class="{'glyphicon-chevron-down': status.open, 'glyphicon-chevron-right': !status.open}"></i>
      </uib-accordion-heading>
      This is just some content to illustrate fancy headings.
    </uib-accordion-group>

    <uib-accordion-group  panel-class="panel-info">
      <uib-accordion-heading >
        OTRAS <i class="pull-right glyphicon" ng-class="{'glyphicon-chevron-down': status.open, 'glyphicon-chevron-right': !status.open}"></i>
      </uib-accordion-heading>
      This is just some content to illustrate fancy headings.
    </uib-accordion-group>
  </uib-accordion>
        <script type="text/ng-template" id="myModalContent.html">
        <div class="modal-header">
            <h3 class="modal-title">Asignación Masiva Auditor</h3>
        </div>
        <div class="modal-body">
        seleccione el auditor para la asignación:
        </b>
            <select id="cboDepartamentos">
                <option value="Mendoza" selected="selected">Juan</option>
                <option value="General Alvear">Diego</option>
                <option value="Godoy Cruz">Gustavo</option>
                <option value="General Alvear">Diego</option>
                <option value="Godoy Cruz">Gustavo</option>
                <option value="General Alvear">Diego</option>
                <option value="Godoy Cruz">Gustavo</option>
                <option value="General Alvear">Diego</option>
                <option value="Godoy Cruz">Gustavo</option>
                <option value="Guaymallén">etc</option>
                                    
            </select>
           </b>
        </div>
        <div class="modal-footer">
            <button class="btn btn-primary" type="button" ng-click="ok()">OK</button>
            <button class="btn btn-warning" type="button" ng-click="cancel()">Cancel</button>
        </div>
        </script>
    </div>
</asp:Content>
