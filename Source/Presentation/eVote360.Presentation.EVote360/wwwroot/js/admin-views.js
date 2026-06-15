(function () {
    'use strict';

    function initHeroDate() {
        var el = document.getElementById('adminHeroDate');
        if (!el) return;
        var now = new Date();
        var opts = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        el.textContent = now.toLocaleDateString('es-DO', opts);
    }

    function animateCounter(el, target, duration) {
        duration = duration || 1800;
        if (target <= 0) { el.textContent = '0'; return; }
        var startTime = null;

        function step(timestamp) {
            if (!startTime) startTime = timestamp;
            var progress = Math.min((timestamp - startTime) / duration, 1);
            var eased = 1 - Math.pow(1 - progress, 3);
            el.textContent = Math.round(target * eased).toLocaleString('es-DO');
            if (progress < 1) requestAnimationFrame(step);
        }

        requestAnimationFrame(step);
    }

    function initCounters() {
        var counters = document.querySelectorAll('.stat-card__value[data-count]');
        if (!counters.length) return;

        if (!('IntersectionObserver' in window)) {
            counters.forEach(function (el) {
                animateCounter(el, parseInt(el.getAttribute('data-count'), 10) || 0);
            });
            return;
        }

        var obs = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    var el = entry.target;
                    animateCounter(el, parseInt(el.getAttribute('data-count'), 10) || 0);
                    obs.unobserve(el);
                }
            });
        }, { threshold: 0.3 });

        counters.forEach(function (c) { obs.observe(c); });
    }

    var _evCurrentError = 0;

    window.evNavigateError = function (dir) {
        var slides = document.querySelectorAll('.ev-error-slide');
        var total = slides.length;
        if (!total) return;

        slides[_evCurrentError].classList.remove('active');
        _evCurrentError = Math.max(0, Math.min(total - 1, _evCurrentError + dir));
        slides[_evCurrentError].classList.add('active');

        var curr = document.getElementById('evErrorCurrent');
        var prev = document.getElementById('evErrorPrev');
        var next = document.getElementById('evErrorNext');

        if (curr) curr.textContent = _evCurrentError + 1;
        if (prev) prev.disabled = _evCurrentError === 0;
        if (next) next.disabled = _evCurrentError === total - 1;
    };

    function initAutoDismiss() {
        var notif = document.getElementById('evTempNotification');
        if (!notif || !notif.classList.contains('ev-notification--success')) return;

        setTimeout(function () {
            notif.style.transition = 'opacity 0.4s ease, transform 0.4s ease';
            notif.style.opacity = '0';
            notif.style.transform = 'translateY(-10px)';
            setTimeout(function () {
                if (notif.parentNode) notif.parentNode.removeChild(notif);
            }, 420);
        }, 5000);
    }

    var _paginators = {};

    function buildPaginationHTML(id, totalPages, currentPage, handler) {
        if (totalPages <= 1) return '';

        var items = [];

        items.push(
            '<li class="page-item ' + (currentPage === 1 ? 'disabled' : '') + '">' +
            '<button type="button" class="page-link" onclick="' + handler + '(\'' + id + '\',' + (currentPage - 1) + ')" aria-label="Anterior">' +
            '<i class="bi bi-chevron-left"></i></button></li>'
        );

        var shown = {};
        for (var i = 1; i <= totalPages; i++) {
            var near = (i >= currentPage - 2 && i <= currentPage + 2);
            var edge = (i === 1 || i === totalPages);
            if (edge || near) {
                shown[i] = true;
            }
        }

        var prev = 0;
        for (var p = 1; p <= totalPages; p++) {
            if (shown[p]) {
                if (prev && p - prev > 1) {
                    items.push('<li class="page-item disabled"><span class="page-link">&hellip;</span></li>');
                }
                items.push(
                    '<li class="page-item ' + (p === currentPage ? 'active' : '') + '">' +
                    '<button type="button" class="page-link" onclick="' + handler + '(\'' + id + '\',' + p + ')">' + p + '</button></li>'
                );
                prev = p;
            }
        }

        items.push(
            '<li class="page-item ' + (currentPage === totalPages ? 'disabled' : '') + '">' +
            '<button type="button" class="page-link" onclick="' + handler + '(\'' + id + '\',' + (currentPage + 1) + ')" aria-label="Siguiente">' +
            '<i class="bi bi-chevron-right"></i></button></li>'
        );

        return '<nav aria-label="Paginación de registros">' +
            '<ul class="pagination pagination-sm justify-content-center">' +
            items.join('') + '</ul></nav>';
    }

    function initTablePagination(tbodyId, wrapId, pageSize) {
        pageSize = pageSize || 10;
        var tbody = document.getElementById(tbodyId);
        var wrap  = document.getElementById(wrapId);
        if (!tbody || !wrap) return;

        var rows = Array.from(tbody.querySelectorAll('tr'));
        var total = rows.length;
        if (total <= pageSize) return;

        var totalPages = Math.ceil(total / pageSize);
        var currentPage = 1;

        function goTo(page) {
            currentPage = Math.max(1, Math.min(totalPages, page));
            var start = (currentPage - 1) * pageSize;
            rows.forEach(function (row, i) {
                row.style.display = (i >= start && i < start + pageSize) ? '' : 'none';
            });
            wrap.innerHTML = buildPaginationHTML(tbodyId, totalPages, currentPage, 'window._evTablePage');
        }

        _paginators[tbodyId] = goTo;
        goTo(1);
    }

    window._evTablePage = function (id, page) {
        if (_paginators[id]) _paginators[id](page);
    };

    function initCardPagination(containerId, wrapId, pageSize, cardSelector) {
        pageSize = pageSize || 5;
        cardSelector = cardSelector || '.summary-card';
        var container = document.getElementById(containerId);
        var wrap      = document.getElementById(wrapId);
        if (!container || !wrap) return;

        var cards = Array.from(container.querySelectorAll(cardSelector));
        var total = cards.length;
        if (total <= pageSize) return;

        var totalPages = Math.ceil(total / pageSize);
        var currentPage = 1;

        function goTo(page) {
            currentPage = Math.max(1, Math.min(totalPages, page));
            var start = (currentPage - 1) * pageSize;
            cards.forEach(function (card, i) {
                card.style.display = (i >= start && i < start + pageSize) ? '' : 'none';
            });
            wrap.innerHTML = buildPaginationHTML(containerId, totalPages, currentPage, 'window._evCardPage');
        }

        _paginators[containerId] = goTo;
        goTo(1);
    }

    window._evCardPage = function (id, page) {
        if (_paginators[id]) _paginators[id](page);
    };

    window.initTablePagination = initTablePagination;
    window.initCardPagination  = initCardPagination;


    function initTableSearch(inputId, tbodyId, paginationWrapId, cardContainerId, cardClass, mobileWrapId) {
        var input = document.getElementById(inputId);
        if (!input) return;

        input.addEventListener('input', function () {
            var term  = this.value.toLowerCase().trim();
            var pWrap = paginationWrapId ? document.getElementById(paginationWrapId) : null;
            var mWrap = mobileWrapId     ? document.getElementById(mobileWrapId)     : null;

            if (tbodyId) {
                var tbody = document.getElementById(tbodyId);
                if (tbody) {
                    Array.from(tbody.querySelectorAll('tr')).forEach(function (row) {
                        row.style.display = !term || row.textContent.toLowerCase().indexOf(term) !== -1 ? '' : 'none';
                    });
                }
            }

            if (cardContainerId && cardClass) {
                var container = document.getElementById(cardContainerId);
                if (container) {
                    Array.from(container.querySelectorAll(cardClass)).forEach(function (card) {
                        card.style.display = !term || card.textContent.toLowerCase().indexOf(term) !== -1 ? '' : 'none';
                    });
                }
            }

            if (pWrap) pWrap.style.display = term ? 'none' : '';
            if (mWrap) mWrap.style.display = term ? 'none' : '';

            if (!term) {
                if (_paginators[tbodyId])        _paginators[tbodyId](1);
                if (_paginators[cardContainerId]) _paginators[cardContainerId](1);
            }
        });
    }

    window.initTableSearch = initTableSearch;


    function initYearCardAnimations() {
        document.querySelectorAll('.year-card').forEach(function (card, i) {
            card.style.animationDelay = (i * 0.055) + 's';
        });
    }

    document.addEventListener('DOMContentLoaded', function () {
        initHeroDate();
        initCounters();
        initAutoDismiss();
        initYearCardAnimations();

        var pageSize = typeof window.summaryPageSize === 'number' ? window.summaryPageSize : 10;
        initTablePagination('summaryTableBody', 'paginationWrap',       pageSize);
        initCardPagination ('summaryCards',     'paginationWrapMobile', 5);
    });

})();
