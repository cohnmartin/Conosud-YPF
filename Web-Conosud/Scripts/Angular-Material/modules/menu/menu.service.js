(function () {

    'use strict';

    angular.module('common.services')
    .factory('menu', ['$location', '$rootScope', 'PageMethodsMenu', '$filter', function ($location, $rootScope, PageMethodsMenu, $filter) {

        var menues;

        PageMethodsMenu.getMenues()
                       .then(function (response) {

                           menues = response.data.d.Menu;


                           for (var i = 0; i < menues.length; i++) {

                               if (menues[i].IdPadre == null) {

                                   var found = $filter('filter')(menues, { IdPadre: menues[i].IdSegMenu }, true);

                                   var hijos = Array();
                                   for (var j = 0; j < found.length; j++) {
                                       hijos.push(
                                       {
                                           name: found[j].Descripcion,
                                           type: 'link',
                                           state: '',
                                           icon: 'fa fa-cog',
                                           target: found[j].Target,
                                           url: found[j].Url,
                                       }
                                       );
                                   }

                                   sections.push({
                                       name: menues[i].Descripcion,
                                       type: 'toggle',
                                       pages: hijos
                                   });
                               }
                           }

                       });

        var sections = [];

        var self;

        return self = {
            sections: sections,

            toggleSelectSection: function (section) {
                self.openedSection = (self.openedSection === section ? null : section);
            },
            isSectionSelected: function (section) {
                return self.openedSection === section;
            },

            selectPage: function (section, page) {
                page && page.url && $location.path(page.url);
                self.currentSection = section;
                self.currentPage = page;
            }
        };

        function sortByHumanName(a, b) {
            return (a.humanName < b.humanName) ? -1 :
            (a.humanName > b.humanName) ? 1 : 0;
        }

    } ])

})();