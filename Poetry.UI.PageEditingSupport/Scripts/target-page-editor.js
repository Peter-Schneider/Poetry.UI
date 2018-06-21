


/* GET PROPERTY */

function getProperty(element) {
    var elementBoundingClientRect = element.getBoundingClientRect();

    return {
        name: element.getAttribute('property-name'),
        objectId: element.getAttribute('object-id'),
        left: elementBoundingClientRect.left,
        top: elementBoundingClientRect.top,
        width: elementBoundingClientRect.width,
        height: elementBoundingClientRect.height
    };
}



/* HOVER EVENT LISTENER */

function addHoverEventListeners() {
    var messageManager = new WindowMessageManager();

    document.body.addEventListener('mouseover', function (event) {
        var target = event.target;

        while (target && !target.getAttribute('property-name')) {
            target = target.parentElement;
        }

        if (!target) {
            return;
        }

        var name = target.getAttribute('property-name');
        var contentId = target.getAttribute('content-id');

        messageManager.send('mouseOverProperty', { name, contentId });
    });

    document.body.addEventListener('mouseout', function (event) {
        var target = event.target;

        while (target && !target.getAttribute('property-name')) {
            target = target.parentElement;
        }

        if (!target) {
            return;
        }

        var name = target.getAttribute('property-name');
        var contentId = target.getAttribute('content-id');

        messageManager.send('mouseOutProperty', { name, contentId });
    });
};

if (document.readyState != 'loading') {
    addHoverEventListeners();
} else {
    document.addEventListener("DOMContentLoaded", addHoverEventListeners);
}



/* UPDATE PROPERTY CONTAINERS */

function updatePropertyContainers() {
    var messageManager = new WindowMessageManager();

    var update = throttle(() => {
        var properties = [...document.querySelectorAll('.poetry-page-editing-property')].map(getProperty);

        messageManager.send('updatePropertyContainers', { properties: properties });
    });

    update();

    window.addEventListener('resize', update, true);
}


if (document.readyState != 'loading') {
    updatePropertyContainers();
} else {
    document.addEventListener("DOMContentLoaded", updatePropertyContainers);
}



/* UPDATE DOCUMENT HEIGHT */

function updateDocumentHeight() {
    var messageManager = new WindowMessageManager();

    var update = throttle(() => {
        messageManager.send('updateDocumentHeight', { documentHeight: document.documentElement.scrollHeight });

        setTimeout(update, 1000);
    });

    update();

    window.addEventListener('resize', update, true);
}

if (document.readyState != 'loading') {
    updateDocumentHeight();
} else {
    document.addEventListener("DOMContentLoaded", updateDocumentHeight);
}



/* THROTTLE */

function throttle(fn, threshhold, scope) {
    threshold = threshhold || 250;
    var last, deferTimer;
    return function () {
        var context = scope || this;

        var now = +new Date,
            args = arguments;
        if (last && now < last + threshhold) {
            // hold on to it
            clearTimeout(deferTimer);
            deferTimer = setTimeout(function () {
                last = now;
                fn.apply(context, args);
            }, threshhold);
        } else {
            last = now;
            fn.apply(context, args);
        }
    };
}