<template>
    <div class="row">
        <div class="col-sm-12 col-lg-6">
            <div class="card">
                <div class="card-header d-flex justify-content-between">
                    <div class="header-title">
                        <h4 class="card-title">Upload IPA</h4>
                    </div>
                    <a href="#">上傳前須知事項</a>
                </div>
                <div class="card-body">
                    <form v-on:submit.prevent="uploadIPA">
                        <div class="form-group">
                            <label for="ipa" class="form-label custom-file-input">Upload IPA file: </label>
                            <input class="form-control" type="file" id="ipa" @change="updateFile">
                        </div>
                        <div class="form-group">
                            <label for="crt" class="form-label custom-file-input">Upload CRT file: </label>
                            <input class="form-control" type="file" id="crt" @change="updateFile">
                        </div>
                        <div class="d-flex justify-content-end">
                            <button type="submit" class="btn btn-primary">Submit</button>
                        </div>
                    </form>
                </div>
            </div>

        </div>
        <div class="col-sm-12 col-lg-6">
            <div class="card">
                <div class="card-header d-flex justify-content-between">
                    <div class="header-title">
                        <h4 class="card-title">Download IPA Here</h4>
                    </div>
                    <a href="#">下載前須知事項</a>
                </div>
                <div class="card-body p-0">
                    <div class="table-responsive mt-4">
                        <table id="basic-table" class="table table-striped mb-0" role="grid">
                            <thead>
                            <tr>
                                <th>IPA Name</th>
                                <th>Functional</th>
                            </tr>
                            </thead>
                            <tbody>
                            <tr v-for="ipa in ipaResultFiles">
                                <td>
                                    <div class="d-flex align-items-center">
                                        <h6>{{ ipa }}</h6>
                                    </div>
                                </td>
                                <td>
                                    <button class="btn btn-icon btn-primary mx-2" data-bs-toggle="modal" 
                                            data-bs-target="#editModal" @click="downloadIpa(ipa)">
                                        <icon-component type="DualTone" icon-name="save" :size="25"></icon-component>
                                    </button>
                                    <button class="btn btn-icon btn-danger mx-2 delete-beacon" @click="deleteIPA(ipa)">
                                        <icon-component type="DualTone" icon-name="trash" :size="25"></icon-component>
                                    </button>
                                </td>
                            </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue"
import {makeToast, ToastColor} from "@/modules/Toast"
import IconComponent from "@/components/icons/IconComponent.vue"

const ipaResultFiles = ref([])
const ipaFile = ref(null)
const crtFile = ref(null)

const getIPAs = async () => {
    try {
        fetch('/api/ipa', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => {
            if (response.ok) {
                return response.json()
            } else {
                throw new Error('Error getting IPAs')
            }
        }).then(data => {
            ipaResultFiles.value = data
        })
    } catch (e) {
        makeToast('Error!', 'Error getting IPAs', ToastColor.Error)
    }
}

const updateFile = (event: any) => {
    if (event.target.id === 'ipa') {
        ipaFile.value = event.target.files[0]
    } else if (event.target.id === 'crt') {
        crtFile.value = event.target.files[0]
    }
}

const uploadIPA = async () => {
    try {
        if (!ipaFile.value || !crtFile.value) {
            makeToast('Error!', 'Please upload both IPA and CRT files', ToastColor.Error)
            return
        }

        const formData = new FormData();
        formData.append('ipa', ipaFile.value);
        formData.append('crt', crtFile.value);
        
        fetch('/api/Ipa', {
            method: 'POST',
            body: formData
        }).then(response => {
            if (response.ok) {
                makeToast('Success!', 'IPA uploaded successfully', ToastColor.Success)
                getIPAs()
            } else {
                throw new Error('Error uploading IPA')
            }
        })
    } catch (e) {
        makeToast('Error!', 'Error uploading IPA', ToastColor.Error)
    }
}

const downloadIpa = async (ipa: string) => {
    try {
        fetch(`/api/Ipa/download/${ipa}`, {
            method: 'GET'
        }).then(response => {
            if (response.ok) {
                return response.blob()
            } else {
                throw new Error('Error downloading IPA')
            }
        }).then(blob => {
            const downloadURL = window.URL.createObjectURL(blob)
            const downloadLink = document.createElement('a')
            downloadLink.href = downloadURL
            downloadLink.setAttribute('download', ipa)
            document.body.appendChild(downloadLink)
            downloadLink.click()
            document.body.removeChild(downloadLink)
            makeToast('Success!', 'IPA downloaded successfully', ToastColor.Success)
        })
    } catch (e) {
        makeToast('Error!', 'Error downloading IPA', ToastColor.Error)
    }
}

const deleteIPA = async (ipa: string) => {
    try {
        fetch(`/api/Ipa/${ipa}`, {
            method: 'DELETE'
        }).then(response => {
            if (response.ok) {
                makeToast('Success!', 'IPA deleted successfully', ToastColor.Success)
                getIPAs()
            } else {
                throw new Error('Error deleting IPA')
            }
        })
    } catch (e) {
        makeToast('Error!', 'Error deleting IPA', ToastColor.Error)
    }
}

onMounted(() => {
    getIPAs()
})
</script>