// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//bấm vào nút thì hiện popup finder
function elfinderDialog(editor) {
    var fm = $('<div />').dialogelfinder({
        url: '/el-finder-file-system/connector',
        baseUrl: "/lib/elfinder/",
        lang: 'vi',
        width: 840,
        height: 450,
        destroyOnClose: true,
        getFileCallback: function (files, fm) {
            console.log(files);
            editor.invoke('editor.insertImage', files.url);
        },
        commandsOptions: {
            getfile: {
                oncomplete: 'close',
                folders: false
            }
        }
    }).dialogelfinder('instance');
}

(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define(['jquery'], factory);
    }
    else if (typeof module === 'object' && module.exports) {
        module.exports = factory(require('jquery'));
    } else {
        factory(window.jQuery);
    }
}(function ($) {
    $.extend($.summernote.plugins, {
        // Tạo plugin tên elfinder
        'elfinder': function (context) {
            var self = this;
            // ui has renders to build ui elements.
            var ui = $.summernote.ui;
            // Tạo nút bấm
            context.memo('button.elfinder', function () {
                var button = ui.button({
                    contents: '<i class="note-icon-picture"/> elFinder',
                    tooltip: 'Quản lý file',
                    click: function () {
                        // Bấm vào nút bấm gọi hàm elfinderDialog
                        elfinderDialog(context);
                    }
                });
                // create jQuery object from button instance.
                var $elfinder = button.render();
                return $elfinder;
            });
            // This methods will be called when editor is destroyed by $('..').summernote('destroy');
            // You should remove elements on `initialize`.
            this.destroy = function () {
                this.$panel.remove();
                this.$panel = null;
            };
        }

    });
}));

$(document).ready(function () {

    $('#summernote,#summernote1').summernote({
        placeholder: '',
        tabsize: 2,
        height: 350,
        toolbar: [
            ['style', ['style']],
            ['font', ['bold', 'underline', 'clear']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link', 'picture', 'video', 'elfinder']],
            ['view', ['fullscreen', 'codeview', 'help']]
        ]
    });


    // Documentation for client options:
    // https://github.com/Studio-42/elFinder/wiki/Client-configuration-options
    var myCommands = elFinder.prototype._options.commands;
    // Not yet implemented commands in elFinder.NetCore
    var disabled = ['callback', 'chmod', 'editor', 'netmount', 'ping', 'search', 'zipdl', 'help'];
    elFinder.prototype.i18.en.messages.TextArea = "Edit";

    $.each(disabled, function (i, cmd) {
        (idx = $.inArray(cmd, myCommands)) !== -1 && myCommands.splice(idx, 1);
    });

    var options = {
        baseUrl: "/lib/elfinder/",
        url: "/el-finder-file-system/connector",
        rememberLastDir: false,
        commands: myCommands,
        lang: 'vi',
        uiOptions: {
            toolbar: [
                ['back', 'forward'],
                ['reload'],
                ['home', 'up'],
                ['mkdir', 'mkfile', 'upload'],
                ['open', 'download'],
                ['undo', 'redo'],
                ['info'],
                ['quicklook'],
                ['copy', 'cut', 'paste'],
                ['rm'],
                ['duplicate', 'rename', 'edit'],
                ['selectall', 'selectnone', 'selectinvert'],
                ['view', 'sort']
            ]
        },
        //onlyMimes: ["image", "text/plain"] // Get files of requested mime types only
        lang: 'vi',
    };




});
