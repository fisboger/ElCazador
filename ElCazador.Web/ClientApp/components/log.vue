<template>
  <el-col :span="24">
    <el-card>
      <div slot="header" class="clearfix">
        <span>Log</span>
      </div>
      <template>
        <el-table v-loading="currentLog.isLoading" :data="currentLog.data" :max-height="height" :default-sort="{prop: 'timestamp', order: 'descending'}" empty-text="Do something please! I'm bored">
          <el-table-column width="200" sortable prop="timestamp" label="Time">
          </el-table-column>
          <el-table-column prop="message" label="Message" min-width="100">
          </el-table-column>
        </el-table>
      </template>
    </el-card>
  </el-col>
</template>

<script>
import { mapActions, mapState, mapMutations } from "vuex";

export default {
  data: function() {
    return {
      height: 0
    };
  },
  computed: {
    ...mapState({
      currentLog: state => state.log
    })
  },
  created() {
    window.addEventListener("resize", this.handleResize);
    this.handleResize();

    this.connection = new this.$signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/LogHub")
      .configureLogging(this.$signalR.LogLevel.Error)
      .build();
  },
  mounted: function() {
    this.connection.start().catch(function(err) {
      console.error(err);
    });
    this.connection.on("AddLogEntry", log => {
      this.$store.commit("ADD_LOG_ENTRY", {
        id: log.id,
        timestamp: log.timestampString,
        name: log.name,
        message: log.formattedMessage,
        parameters: log.parameters
      });
    });
  },
  destroyed() {
    window.removeEventListener("resize", this.handleResize);
  },
  methods: {
    handleResize() {
      this.height = window.innerHeight - 470;
    }
  }
};
</script>

<style>
</style>