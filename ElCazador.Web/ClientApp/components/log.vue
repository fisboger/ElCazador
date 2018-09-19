<template>
  <el-col :span="24">
    <el-card>
      <div slot="header" class="clearfix">
        <span>Log</span>
      </div>
      <template>
        <el-table v-loading="log.isLoading" :data="log.data" :max-height="log.height" :default-sort="{prop: 'timestamp', order: 'descending'}">
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
export default {
  data: function() {
    return {
      log: {
        isLoading: false,
        data: [
          {
            timestamp: "2018-05-01 00:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 01:00:00",
            message:
              "Got NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 02:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 03:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 04:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 05:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 06:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 07:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 08:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 09:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 10:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 11:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 12:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 13:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 14:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 15:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 16:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 17:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 00:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 01:00:00",
            message:
              "Got NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 02:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 03:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 04:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 05:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 06:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 07:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 08:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 09:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 10:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 11:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 12:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 13:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 14:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 15:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 16:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 17:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 00:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 01:00:00",
            message:
              "Got NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 02:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 03:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 04:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 05:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 06:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 07:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 08:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 09:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 10:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 11:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 12:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 13:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 14:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 15:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 16:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 17:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 00:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 01:00:00",
            message:
              "Got NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 02:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 03:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 04:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 05:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 06:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 07:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 08:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 09:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 10:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 11:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 12:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 13:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 14:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 15:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 16:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 17:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 00:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 01:00:00",
            message:
              "Got NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 02:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 03:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 04:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 05:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 06:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 07:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 08:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 09:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 10:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 11:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 12:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 13:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 14:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 15:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 16:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 17:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 00:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 01:00:00",
            message:
              "Got NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 02:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 03:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 04:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 05:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 06:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 07:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 08:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 09:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 10:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 11:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 12:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 13:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 14:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 15:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 16:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 17:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 00:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 01:00:00",
            message:
              "Got NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 02:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 03:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 04:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 05:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 06:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 07:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 08:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 09:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 10:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 11:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 12:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 13:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 14:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 15:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 16:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 17:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 00:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 01:00:00",
            message:
              "Got NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 02:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 03:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 04:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 05:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 06:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 07:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 08:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 09:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 10:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 11:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 12:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 13:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 14:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 15:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 16:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 17:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 00:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 01:00:00",
            message:
              "Got NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxxGot NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 02:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 03:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 04:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 05:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 06:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 07:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 08:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 09:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 10:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 11:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 12:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 13:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 14:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 15:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 16:00:00",
            message: "Got NTLM hash for user xxx"
          },
          {
            timestamp: "2018-05-01 17:00:00",
            message: "Got NTLM hash for user xxx"
          }
        ],
        height: 0
      }
    };
  },
  created() {
    window.addEventListener("resize", this.handleResize);
    this.handleResize();
  },
  destroyed() {
    window.removeEventListener("resize", this.handleResize);
  },
  methods: {
    handleResize() {
      this.log.height = window.innerHeight - 470;
    }
  }
};
</script>

<style>
</style>