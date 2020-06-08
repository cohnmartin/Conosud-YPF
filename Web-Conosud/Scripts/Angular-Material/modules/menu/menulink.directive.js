(function(){
  'use strict';

  angular.module('common.directives')
    .run(['$templateCache', function ($templateCache) {
        $templateCache.put('partials/menu-link.tmpl.html',
        '<md-button style="background-color: #0467a2 !important;" href="{{section.url}}" ng-class="{\'{{section.icon}}\' : true}" ui-sref-active="active" \n' +
        '  ui-sref="{{section.state}}" ng-click="focusSection()">\n' +
        '  &nbsp;&nbsp;<span class="md-body-2" style="font-size: 13px;font-family: monospace;">{{section.name}}</span>\n' +
        '  <span  class="md-visually-hidden "\n' +
        '    ng-if="isSelected()">\n' +
        '    current page\n' +
        '  </span>\n' +
        '</md-button>\n' +
        '');
    }])
    .directive('menuLink', function () {
      return {
        scope: {
          section: '='
        },
      templateUrl: 'partials/menu-link.tmpl.html',
        link: function ($scope, $element) {
          var controller = $element.parent().controller();

          $scope.focusSection = function () {
            // set flag to be used later when
            // $locationChangeSuccess calls openPage()
            controller.autoFocusContent = true;
          };
        }
      };
    })
})();