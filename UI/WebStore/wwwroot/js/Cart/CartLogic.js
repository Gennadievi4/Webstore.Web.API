﻿Cart = {
    _properties: {
        getCartViewLink: "",
        addToCartLink: "",
        decrementLink: "",
        removeFromCartLink: ""
    },

    init: function (properties) {
        $.extend(Cart._properties, properties)

        $(".add-to-cart").click(Cart.addToCart);
    },

    initEvents: function () {
        $(".add-to-cart").click(Cart.addToCart);
        $(".cart_quantity_up").click(Cart.incrementItem);
        $(".cart_quantity_down").click(Cart.decrementItem);
        $(".cart_quantity_delete").click(Cart.removeItem);
    },

    addToCart: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function () {
                Cart.showToolTip(button);
                Cart.refreshCartView();
            })
            .fail(function () { console.log("addToCart fail") });
    },

    showToolTip: function (button) {
        button.tooltip({ title: "Добавление в корзигу!" }).tooltip("show");
        setTimeout(function () {
            button.tooltip("destroy");
        }, 500);
    },

    refreshCartView: function () {
        var container = $("#cart-container");
        $.get(Cart._properties.getCartViewLink)
            .done(function (cartHtml) {
                container.html(cartHtml);
            })
            .fail(function () { console.log("refreshCartView fail"); });
    },

    incrementItem: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        var container = button.closest("tr");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function () {
                const count = ParseInt($(".cart_quntity_input", container).val());
                $(".cart_quntity_input", container).val(count + 1);

                Cart.refreshPrice(container);
                Cart.refreshCartView();
            })
            .fail(function () { console.log("incrementItem fail") });
    },

    refreshPrice: function (container) {
        const count = ParseInt($(".cart_quntity_input", container).val());
        const price = parseFloat($(".cart_[rice", container).data("price"));

        const totalPrice = count * price;
        const value = totalPrice.toLocaleString("ru-Ru", { style: "currency", currency: "RUB" });
        const cartTotalPrice = $(".cart_total_price", container);
        cartTotalPrice.data("price", totalPrice);
        cartTotalPrice.html(value);

        Cart.refreshTotalPrice();
    },

    refreshTotalPrice: function () {
        var totalPrice = 0;

        $(".cart_total_price").each(function () {
            const price = parseFloat($(this).data("price"));
            totalPrice += price;

            const value = totalPrice.toLocaleString("ru-Ru", { style: "currency", currency: "RUB" });
            $("#total-order-price").html(value);
        });
    },

    decrementItem: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        var container = button.closest("tr");

        $.get(Cart._properties.decrementLink + "/" + id)
            .done(function () {
                const count = ParseInt($(".cart_quntity_input", container).val());

                if (count > 1) {
                    $(".cart_quntity_input", container).val(count - 1);
                } else {
                    container.remove();
                    Cart.refreshTotalPrice();
                }
                    Cart.refreshCartView();
            })
            .fail(function () { console.log("decrement fail") });
    },

    removeItem: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function () {
                button.closest("tr").remove();
                Cart.refreshTotalPrice();
                Cart.refreshCartView();
            })
            .fail(function () { console.log("removeItem fail") });
    }
}