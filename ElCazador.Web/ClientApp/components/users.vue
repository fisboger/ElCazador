<template>
    <div>
        <el-dialog title="Add user" :visible.sync="addUserVisible" width="20%">
            <el-form :model="form">
                <el-form-item label="Username" :label-width="formLabelWidth">
                    <el-input v-model="form.name" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="Password" :label-width="formLabelWidth">
                    <el-input v-model="form.name" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="Password type" :label-width="formLabelWidth">
                    <el-select v-model="form.region" placeholder="Password type">
                        <el-option label="NTLM" value="ntlm"></el-option>
                        <el-option label="NetNTLMv2" value="netntlmv2"></el-option>
                        <el-option label="Clear-text" value="clearText"></el-option>
                    </el-select>
                </el-form-item>
            </el-form>
            <span slot="footer" class="dialog-footer">
                <el-button @click="addUserVisible = false">Cancel</el-button>
                <el-button type="primary" @click="addUserVisible = false">Confirm</el-button>
            </span>
        </el-dialog>

        <el-col :span="12" class="spaced-col">
            <el-card class="box-card">
                <div slot="header" class="clearfix">
                    <span>Users</span>
                    <el-button style="float: right;" @click="addUserVisible = true" type="primary" icon="el-icon-plus" size="mini" circle></el-button>
                </div>
                <template>
                    <el-table v-loading="users.isLoading" :data="users.data" style="min-height:200px" :default-sort="{prop: 'timestamp', order: 'descending'}" max-height="200">
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
                                <el-button class="zero-padding" type="text" icon="el-icon-edit" circle></el-button>
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
      addUserVisible: false,
      form: {},
      formLabelWidth: "120px"
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
      console.log(target);
      this.$store.commit("ADD_USER", {
        id: user.ID,
        ipAddress: user.IPAddress,
        domain: user.Domain,
        hash: user.NetNTHash,
        clearTextPw: user.clearTextPW
      });
    });
  },
  methods: {
      add: function(event) {
          this.connection.invoke("AddUser", {
            ipAddress: user.IPAddress,
            domain: user.Domain,
            hash: user.NetNTHash,
            clearTextPw: user.clearTextPW
          }).catch(function(err) {
          console.error(err);
        });
      }
  }
};
</script>

<style>
</style>