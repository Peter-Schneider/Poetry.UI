


/* PAGE EDITOR */

class PageEditor {
    constructor(model, url) {
        if (url.indexOf('/') == 0) {
            url = url.substr(1);
        }

        this.element = document.createElement('page-editor');

        this.frame = document.createElement('iframe');
        this.frame.classList.add('page-editor-frame');

        this.frame.src = 'PageEditing/EditPage/' + url;
        this.element.appendChild(this.frame);

        this.containers = {};

        var message = new WindowMessageManager(this.element);

        message.on('updateDocumentHeight', data => this.frame.style.height = data.documentHeight + 'px');
        message.on('updatePropertyContainers',
            data =>
                data.properties.forEach(property =>
                    (this.containers[property.name] || (this.containers[property.name] = new PropertyContainer(property).appendTo(this.element))).update(property)
                )
        );
        message.on('mouseOverProperty', data => this.containers[data.name] && this.containers[data.name].element.classList.add('hover'));
        message.on('mouseOutProperty', data => this.containers[data.name] && this.containers[data.name].element.classList.remove('hover'));
    }

    appendTo(element) {
        element.appendChild(this.element);
    }
}



/* PROPERTY CONTAINER */

class PropertyContainer {
    constructor(property, container) {
        this.element = document.createElement('div');

        this.element.classList.add('property');
        this.element.setAttribute('property-name', property.name);

        var title = document.createElement('div');
        title.className = 'property-title';
        title.innerText = property.name;
        this.element.appendChild(title);

        var border = document.createElement('div');
        border.className = 'property-border';
        this.element.appendChild(border);
    }

    update(property) {
        var padding = 2;

        this.top = property.top - padding;
        this.left = property.left - padding;
        this.width = property.width + padding + padding;
        this.height = property.height + padding + padding;

        this.element.style.left = this.left + 'px';
        this.element.style.top = this.top + 'px';
        this.element.style.width = this.width + 'px';
        this.element.style.height = this.height + 'px';

        return this;
    }

    appendTo(element) {
        element.appendChild(this.element);

        return this;
    }

    remove() {
        this.element.parentElement.removeChild(this.element);
    }
}



/* EDIT PROPERTY */

var currentEditor = null;
var propertyBlurDetector = null;
var pageEditingIframe = null;

window.addEventListener('message', function (event) {
    if (!pageEditingIframe) {
        return;
    }

    var data = JSON.parse(event.data);

    if (data.action != 'editProperty') {
        return;
    }

    if (!propertyBlurDetector) {
        propertyBlurDetector = document.createElement('div');
        propertyBlurDetector.classList.add('property-blur-detector');
        propertyBlurDetector.addEventListener('click', function () {
            if (currentEditor) {
                currentEditor.hide();
                currentEditor = null;
                propertyBlurDetector.style.display = 'none';
            }
        });
        pageEditingContainer.appendChild(propertyBlurDetector);
    }

    currentEditor = sweek.editors[data.editor];
    currentEditor.show(data.contentId, data.name, JSON.parse(data.value), propertyContainers[data.name].element, function (value) {
        setPropertyValue(data.contentId, data.name, value);
    });
    propertyBlurDetector.style.display = 'block';
}, false);



/* SET PROPERTY VALUE */

function setPropertyValue(contentId, name, value) {
    sweek.pendingChanges.push({
        contentId: contentId,
        propertyName: name,
        propertyValue: value
    });
}

window.addEventListener('message', function (event) {
    if (!pageEditingIframe) {
        return;
    }

    var data = JSON.parse(event.data);

    if (data.action != 'setPropertyValue') {
        return;
    }

    setPropertyValue(data.contentId, data.name, data.value);
}, false);



/* MOUSE OVER/OUT ON PROPERTIES */

window.addEventListener('message', function (event) {
    if (!pageEditingIframe) {
        return;
    }

    var data = JSON.parse(event.data);

    if (data.action != 'mouseOverProperty' && data.action != 'mouseOutProperty') {
        return;
    }

    var propertyContainer = propertyContainers[data.name];

    if (data.action == 'mouseOverProperty') {
        propertyContainer.element.classList.add('hover');
    }

    if (data.action == 'mouseOutProperty') {
        propertyContainer.element.classList.remove('hover');
    }
}, false);



/* CREATE PROPERTY CONTAINER */

function createPropertyContainer(property) {
    var container = {};


    propertyContainers[property.name] = container;

    return container;
}



/* UPDATE PROPERTY CONTAINERS */

var propertyContainers = {};

function updatePropertyContainers(properties, single) {
    properties.forEach(function (property) {
        if (!propertyContainers[property.name]) {
            propertyContainers[property.name] = new PropertyContainer(property);
        }

        propertyContainers[property.name].update(property);
    });
}