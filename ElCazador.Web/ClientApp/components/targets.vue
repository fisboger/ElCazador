<template>
  <div>
    <!-- Add/Edit target -->
    <el-dialog title="Add target" :visible.sync="targetDialogVisible" width="20%">
      <el-form :model="form">
        <el-form-item label="Host" :label-width="formLabelWidth">
          <el-input v-model="form.hostname" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="IP" :label-width="formLabelWidth">
          <el-input v-model="form.ip" autocomplete="off"></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="targetDialogVisible = false">Cancel</el-button>
        <el-button type="primary" @click="(targetDialogAction == 'ADD') ? add() : edit()">Confirm</el-button>
      </span>
    </el-dialog>

    <!-- Dump target -->
    <el-dialog title="Dump target" :visible.sync="dumpDialogVisible" width="20%">
      <el-form :model="form">
        <el-form-item label="Host" :label-width="formLabelWidth">
          <el-input v-model="form.hostname" autocomplete="off" disabled></el-input>
        </el-form-item>
        <input type="hidden" v-model="form.targetKey" />
        <el-form-item label="User" :label-width="formLabelWidth">
          <el-select v-model="form.userKey" clearable placeholder="Select">
            <el-option v-for="user in currentUsers.data" :key="user.key" :label="user.username" :value="user.key">
            </el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="dumpDialogVisible = false">Cancel</el-button>
        <el-button type="primary" @click="dump()">Dump</el-button>
      </span>
    </el-dialog>

    <el-col :span="12" class="spaced-col">
      <el-card class="box-card">
        <div slot="header" class="clearfix">
          <span>Targets</span>
          <el-button style="float: right;" @click="prepareAdd()" type="primary" icon="el-icon-plus" size="mini" circle></el-button>
        </div>
        <template>
          <el-table v-loading="currentTargets.isLoading" :data="currentTargets.data" style="min-height:200px" empty-text="The challenge is givecpr#" max-height="200">
            <el-table-column sortable prop="hostname" label="Host">
            </el-table-column>
            <el-table-column sortable prop="ip" label="IP" min-width="100">
            </el-table-column>
            <el-table-column sortable prop="dumped" label="Dumped">
              <template slot-scope="scope">
                <i v-if="scope.row.dumped" class="el-icon-check"></i>
                <i v-else class="el-icon-close success"></i>
              </template>
            </el-table-column>
            <el-table-column fixed="right" align="right" min-width="40">
              <template slot-scope="scope">
                <el-button @click="prepareEdit(scope.$index)" class="zero-padding" type="text" icon="el-icon-edit" circle></el-button>
              </template>
            </el-table-column>
            <el-table-column fixed="right" align="right" min-width="40">
              <template slot-scope="scope">
                <el-button @click="prepareDump(scope.$index)" class="zero-padding" type="text">Dump</el-button>
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
  data: function() {
    return {
      targetDialogVisible: false,
      dumpDialogVisible: false,
      targetDialogAction: "ADD",
      form: {},
      formLabelWidth: "120px"
    };
  },
  computed: {
    ...mapState({
      currentTargets: state => state.targets,
      currentUsers: state => state.users
    })
  },
  created: function() {
    this.connection = new this.$signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/TargetHub")
      .configureLogging(this.$signalR.LogLevel.Error)
      .build();
  },
  mounted: function() {
    this.connection.start().catch(function(err) {
      console.error(err);
    });

    this.connection.on("AddTarget", target => {
      this.$store.commit("ADD_TARGET", {
        key: target.key,
        id: target.id,
        timestamp: target.timestamp,
        hostname: target.hostname,
        ip: target.ip,
        dumped: target.dumped
      });
    });
  },
  methods: {
    add: function(event) {
      this.connection
        .invoke("AddTarget", {
          hostname: this.form.hostname,
          ip: this.form.ip,
          dumped: false
        })
        .catch(function(err) {
          console.error(err);
        });

      this.targetDialogVisible = false;
    },
    edit: function(event) {
      this.$store.commit("EDIT_TARGET", this.form);
      this.form = {};

      this.targetDialogVisible = false;
    },
    prepareEdit: function(index) {
      var element = this.currentTargets.data[index];
      this.form = {
        id: element.id,
        hostname: element.hostname,
        ip: element.ip,
        timestamp: element.timestamp,
        dumped: element.dumped
      };
      this.targetDialogAction = "EDIT";
      this.targetDialogVisible = true;
    },

    prepareDump: function(index) {
      var element = this.currentTargets.data[index];
      this.form = {
        hostname: element.hostname,
        targetKey: element.key,
        userKey: ""
      };

      this.dumpDialogVisible = true;
    },

    prepareAdd: function() {
      this.form = {};

      this.targetDialogAction = "ADD";
      this.targetDialogVisible = true;
    },
    send: function() {},
    dump: function() {
      this.connection
        .invoke("dumpTarget", {
          userKey: this.form.userKey,
          targetKey: this.form.targetKey
        })
        .catch(function(err) {
          console.error(err);
        });

        this.dumpDialogVisible = false;
    }
  }
};
</script>

<style>
</style>