import portal from '../../../Portal/Scripts/portal.js';
import App from '../../../Portal/Scripts/app.js';
import Blade from '../../../Portal/Scripts/blade.js';
import translations from '../../../Translation/Scripts/translation.js';
import BladeCloseButton from '../../../Portal/Scripts/close-button.js';
import PortalButton from '../../../Portal/Scripts/button.js';
import DataTable from '../../../DataTable/Scripts/data-table.js';
import ContextMenu from '../../../Poetry.UI.ContextMenu/Scripts/context-menu.js';
import PageEditor from '../../../PageEditing/Scripts/page-editor.js';
import FormBuilder from '../../../Form/Scripts/form-builder.js';
import FormFieldProvider from '../../../Form/Scripts/form-field-provider.js';
import formFieldTypes from '../../../Form/Scripts/form-field-types.js';



/* APP */

portal.addApp(new class extends App {
    constructor() {
        super('Product');

        this.translations = translations.scopeTo('Product');
    }

    open() {
        if (this.blades.length) {
            return;
        }

        this.openBlade(new ListProducts(this));
    }
});



/* LIST */

class ListProducts extends Blade {
    constructor(app) {
        super();

        this.setTitle(app.translations.get('AllProducts'), new BladeCloseButton(app, this));

        var dataTable;

        this.setToolbar(
            new PortalButton(app.translations.get('New'), () => app.openBlade(new EditProduct(app).onClose(message => dataTable.update()), this)),
        );

        this.setContent(
            dataTable = new DataTable()
                .setBackend('product')
                .addColumn(c => c.setHeader(element => app.translations.get('ArticleNo')).setContent(item => item.Item.ArticleNo).setSorting('ArticleNo', true))
                .addColumn(c => c.setHeader(element => app.translations.get('Name')).setContent(item => item.Item.Name).setSorting('Name', true))
                .addColumn(c => c.setActionColumn().setContent((item, dataTable) => new PortalButton(app.translations.get('Edit'), () => app.openBlade(new EditProduct(app, item.Item).onClose(message => dataTable.update()), this))))
                .addColumn(c => c.setActionColumn().setHeader(dataTable => new ContextMenu().addItem(i => i.setText('Copy as Excel').onClick(() => new DataTableCopyAsTabSeparated(dataTable).copy().then(() => alert('Copied to clipboard'))))).setContent(item => new PortalButton(app.translations.get('Open'), () => app.openBlade(new EditProductOnPage(app, item.Item, item.Url), this))))
        );
    }
}



/* EDIT PRODUCT ON PAGE */

class EditProductOnPage extends Blade {
    constructor(app, item, url) {
        super();

        this.setFullscreen();

        this.setTitle(app.translations.get('Edit') + ' ' + item.Name, new BladeCloseButton(app, this))
        this.setCustomContent(new PageEditor(item, url));
    }
}



/* EDIT */

class EditProduct extends Blade {
    constructor(app, item) {
        super();

        if (!item) {
            item = {};
        }

        this.app = app;
        this.formBuilder = new FormBuilder;
        this.formFieldTypes = formFieldTypes;
        this.formFieldProvider = new FormFieldProvider();
        this.formFields = this.formFieldProvider.getFor('product');

        this.setTitle(app.translations.get('New'), new BladeCloseButton(app, this));

        var save = () => {
            return fetch('DomainObjects/Product/Save', {
                credentials: 'include',
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(item)
            });
        }

        this.setContent();

        this.setToolbar(
            new PortalButton(app.translations.get('Save'), () => save().then(() => app.closeBlade(this, 'saved'))),
            new PortalButton(app.translations.get('Cancel'), () => app.closeBlade(this)),
        );

        this.formFields
            .then(formFields => this.setContent(this.formBuilder.build(item, formFields, this.formFieldTypes, this.app.translations)));
    }
}