<template>
    <div>
        <el-dialog title="Add user" :visible.sync="userDialogVisible" width="20%">
            <el-form :model="form">
              <el-form-item label="Domain" :label-width="formLabelWidth">
                    <el-input v-model="form.domain" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="Username" :label-width="formLabelWidth">
                    <el-input v-model="form.username" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="Password" :label-width="formLabelWidth">
                    <el-input v-model="form.password" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="Password type" :label-width="formLabelWidth">
                    <el-select v-model="form.passwordType" placeholder="Password type">
                        <el-option sel label="NTLM" value="ntlm"></el-option>
                        <el-option label="Clear-text" value="clearText"></el-option>
                    </el-select>
                </el-form-item>
            </el-form>
            <span slot="footer" class="dialog-footer">
                <el-button @click="userDialogVisible = false">Cancel</el-button>
                <el-button type="primary" @click="(userDialogAction == 'ADD') ? add() : edit()">Confirm</el-button>
            </span>
        </el-dialog>

        <el-dialog title="Hashes" :visible.sync="hashesDialogVisible" width="20%">
            <el-form :model="form">
                <el-input
                type="textarea"
                :rows="6"
                v-model="hashes">
              </el-input>
            </el-form>
            <span slot="footer" class="dialog-footer">
                <el-button @click="hashesDialogVisible = false">Close</el-button>
            </span>
        </el-dialog>

        <el-col :span="12" class="spaced-col">
            <el-card class="box-card">
                <div slot="header" class="clearfix">
                    <span>Users</span>
                    <el-button @click="dumpHashes()" type="primary" size="mini">Dump hashes</el-button>
                    <el-button style="float: right;" @click="prepareAdd()" type="primary" icon="el-icon-plus" size="mini" circle></el-button>
                </div>
                <template>
                    <el-table v-loading="currentUsers.isLoading" :data="currentUsers.data" style="min-height:200px" :default-sort="{prop: 'timestamp', order: 'descending'}" max-height="200" empty-text="Hva' så håndværker?">
                        <!-- <el-table-column sortable prop="timestamp" label="Time">
                  </el-table-column> -->
                        <el-table-column sortable prop="username" label="User" min-width="100">
                        </el-table-column>
                        <el-table-column sortable prop="hash" label="Hash" min-width="300">
                        </el-table-column>
                        <el-table-column sortable prop="clearTextPw" label="Clear-text" min-width="110">
                            <template slot-scope="scope">
                                <i v-if="scope.row.clearTextPw" class="el-icon-check"></i>
                                <i v-else class="el-icon-close success"></i>
                            </template>
                        </el-table-column>

                        <el-table-column fixed="right" align="right" min-width="40">
                            <template slot-scope="scope">
                                <el-button @click="prepareEdit(scope.$index)" class="zero-padding" type="text" icon="el-icon-edit" circle></el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                </template>
            </el-card>
        </el-col>
    </div>
</template>

<script>
import { mapActions, mapState, mapMutations } from "vuex";

export default {
  data() {
    return {
      userDialogVisible: false,
      hashesDialogVisible: false,
      userDialogAction: "ADD",
      form: {},
      formLabelWidth: "120px",
      hashes: ""
    };
  },
  computed: {
    ...mapState({
      currentUsers: state => state.users
    })
  },
  created: function() {
    this.connection = new this.$signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/UserHub")
      .configureLogging(this.$signalR.LogLevel.Error)
      .build();
  },
  mounted: function() {
    this.connection.start().catch(function(err) {
      console.error(err);
    });

    this.connection.on("AddUser", user => {
      this.$store.commit("ADD_USER", {
        key: user.key,
        domain: user.domain,
        ipAddress: user.ipAddress,
        username: user.username,
        hash: user.hash,
        isClearText: user.isClearText,
        hashcatFormat: user.hashcatFormat
      });
    });
  },
  methods: {
    add: function(event) {
      this.connection
        .invoke("AddUser", {
          username: this.form.username,
          hash: this.form.password,
          domain: this.form.domain,
          passwordType: this.form.passwordType
        })
        .catch(function(err) {
          console.error(err);
        });

      this.userDialogVisible = false;
    },
    edit: function(event) {
      this.$store.commit("EDIT_USER", this.form);
      this.form = {};

      this.userDialogVisible = false;
    },
    prepareEdit: function(index) {
      var element = this.currentUsers.data[index];
      this.form = {
        key: element.key,
        username: element.username,
        password: element.hash
      };

      this.userDialogAction = "EDIT";
      this.userDialogVisible = true;
    },
    prepareAdd: function() {
      this.form = {};

      this.userDialogAction = "ADD";
      this.userDialogVisible = true;
    },
    dumpHashes: function() {
      this.hashes = "";
      this.currentUsers.data.forEach(function(user) {
        if(user.hashcatFormat != null && user.hashcatFormat != "") {
          this.hashes += user.hashcatFormat + "\n";
        }
      });

      this.hashesDialogVisible = true;
    }
  }
};
</script>

<style>
</style>