<template>
    <div class="iq-navbar-header" style="height: 215px">
        <b-container fluid class="iq-container">
            <b-row>
                <b-col md="12">
                    <div class="d-flex justify-content-between align-items-center flex-wrap">
                        <div>
                            <h1>{{title}}</h1>
                            <p>{{description}}</p>
                        </div>
                        <div>
                            <a :href="linkUrl" class="btn btn-link btn-soft-light">
                                {{linkText}}
                            </a>
                        </div>
                    </div>
                </b-col>
            </b-row>
        </b-container>
        <div class="iq-header-img">
            <img src="@/assets/images/dashboard/top-header.png" alt="header" class="theme-color-default-img img-fluid w-100 h-100 animated-scaleX" />
            <img src="@/assets/images/dashboard/top-header1.png" alt="header" class="theme-color-purple-img img-fluid w-100 h-100 animated-scaleX" />
            <img src="@/assets/images/dashboard/top-header2.png" alt="header" class="theme-color-blue-img img-fluid w-100 h-100 animated-scaleX" />
            <img src="@/assets/images/dashboard/top-header3.png" alt="header" class="theme-color-green-img img-fluid w-100 h-100 animated-scaleX" />
            <img src="@/assets/images/dashboard/top-header4.png" alt="header" class="theme-color-yellow-img img-fluid w-100 h-100 animated-scaleX" />
            <img src="@/assets/images/dashboard/top-header5.png" alt="header" class="theme-color-pink-img img-fluid w-100 h-100 animated-scaleX" />
        </div>
    </div>
</template>
<script>
export default {
    props: {
        title: {
            type: String,
            default: 'System Dashboard'
        },
        description: {
            type: String,
            default: 'Welcome to the system dashboard. Here you can view all the important information about your system.'
        },
        linkUrl: {
            type: String,
            default: '/'
        },
        linkText: {
            type: String,
            default: 'Back to Dashboard'
        }
    }
}
</script>
