<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TestSideNav.aspx.cs" Inherits="TestSideNav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var app = angular.module('myApp', ['ngMaterial']);
        app.controller('MyController', function ($scope, $mdSidenav) {
            $scope.openLeftMenu = function () {
                $mdSidenav('left').toggle();
            };
        });
    </script>
    <div id="ng-app" ng-app="myApp">
        <div layout="row" ng-controller="MyController">
            <md-sidenav md-component-id="left" class="md-sidenav-left" md-disable-backdrop="false" >
                Left Nav!
            </md-sidenav>
            <md-content>
                Center Content
                <md-button ng-click="openLeftMenu()">
                    Open Left Menu
                </md-button>
            </md-content>
            <md-sidenav md-component-id="right" md-is-locked-open="$mdMedia('min-width: 333px')" class="md-sidenav-right">
                    <md-input-container>
                        <label for="testInput">Test input</label>
                        <input id="testInput" type="text" ng-model="data" md-autofocus>
                    </md-input-container>
            </md-sidenav>
        </div>
    </div>
</asp:Content>
