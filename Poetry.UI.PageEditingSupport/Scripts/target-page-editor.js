


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
    document.body.addEventListener('mouseover', function (event) {
        var target = event.target;

        while (target && !target.getAttribute('property-name')) {
            target = target.parentElement;
        }

        if (!target) {
            return;
        }

        var message = JSON.stringify({
            action: 'mouseOverProperty',
            name: target.getAttribute('property-name'),
            contentId: target.getAttribute('content-id')
        });

        parent.postMessage(message, '*');
    });

    document.body.addEventListener('mouseout', function (event) {
        var target = event.target;

        while (target && !target.getAttribute('property-name')) {
            target = target.parentElement;
        }

        if (!target) {
            return;
        }

        var message = JSON.stringify({
            action: 'mouseOutProperty',
            name: target.getAttribute('property-name'),
            contentId: target.getAttribute('content-id')
        });

        parent.postMessage(message, '*');
    });
};

if (document.readyState != 'loading') {
    addHoverEventListeners();
} else {
    document.addEventListener("DOMContentLoaded", addHoverEventListeners);
}



/* UPDATE PROPERTY CONTAINERS */

var updatePropertyContainers = throttle(function () {
    var properties = [...document.querySelectorAll('.poetry-page-editing-property')].map(getProperty);

    var message = JSON.stringify({
        action: 'updatePropertyContainers',
        properties: properties
    });

    parent.postMessage(message, '*');
});

if (document.readyState != 'loading') {
    updatePropertyContainers();
} else {
    document.addEventListener("DOMContentLoaded", updatePropertyContainers);
}

window.addEventListener('resize', updatePropertyContainers, true);



/* UPDATE DOCUMENT HEIGHT */

var updateDocumentHeight = throttle(function () {
    var message = JSON.stringify({
        action: 'updateDocumentHeight',
        documentHeight: Math.max(document.documentElement.scrollHeight)
    });

    parent.postMessage(message, '*');

    setTimeout(updateDocumentHeight, 1000);
});

if (document.readyState != 'loading') {
    updateDocumentHeight();
} else {
    document.addEventListener("DOMContentLoaded", updateDocumentHeight);
}

window.addEventListener('resize', updateDocumentHeight, true);



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